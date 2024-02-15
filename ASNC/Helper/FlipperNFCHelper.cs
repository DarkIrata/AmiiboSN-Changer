using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ASNC.Helper
{
    public static class FlipperNFCHelper
    {
        private const int TotalPages = 135; // 0 - 134
        private const int PwdPage = 133;
        private const int PageDataLength = 0x04;
        private const int NfcLength = 0x21C;

        public const string Filetype = ".nfc";

        public static string ToNfc(ReadOnlySpan<byte> encryptedBin)
        {
            if (encryptedBin.Length < NfcLength)
            {
                throw new ArgumentException($"Given bin data is less than '{NfcLength}' Bytes");
            }

            var uidBytes = new byte[]
            {
                encryptedBin[0],
                encryptedBin[1],
                encryptedBin[2],
                encryptedBin[4], // Yes, 4 not 3. Thats why we dont slice it.
                encryptedBin[5],
                encryptedBin[6],
                encryptedBin[7],
            };

            var uid = GetBytesAsStringFormated(uidBytes);
            var zeroedSignature = string.Concat(Enumerable.Repeat("00 ", 32)).Trim();
            var pwdBytes = CalculatePwd(uidBytes);

            var sb = new StringBuilder();
            sb.AppendLine("Filetype: Flipper NFC device");
            sb.AppendLine("Version: 2");
            sb.AppendLine("Device type: NTAG215");
            sb.AppendLine($"UID: {uid}");
            sb.AppendLine("ATQA: 44 00"); // Common value
            sb.AppendLine("SAK: 00"); // Common value
            sb.AppendLine($"Signature: {zeroedSignature}"); // Common value
            sb.AppendLine("Mifare version: 00 04 04 02 01 00 11 03"); // Common value
            sb.AppendLine("Counter 0: 0"); // Common value
            sb.AppendLine("Tearing 0: 00"); // Common value
            sb.AppendLine("Counter 1: 0"); // Common value
            sb.AppendLine("Tearing 1: 00"); // Common value
            sb.AppendLine("Counter 2: 0"); // Common value
            sb.AppendLine("Tearing 2: 00"); // Common value
            sb.AppendLine($"Pages total: {TotalPages}"); // 135 * 4 = 540 Bytes. Size of NTAG215

            for (int i = 0; i < TotalPages; i++)
            {
                var pageBytes = encryptedBin.Slice(i * PageDataLength, PageDataLength); // 4 Bytes per page
                if (i == PwdPage)
                {
                    pageBytes = pwdBytes;
                }
                var pageData = GetBytesAsStringFormated(pageBytes);
                sb.AppendLine($"Page {i}: {pageData}"); // Common value
            }

            return sb.ToString();
        }

        private static string GetBytesAsStringFormated(ReadOnlySpan<byte> bytes)
            => Regex.Replace(Convert.ToHexString(bytes), ".{2}", "$0 ").Trim();

        // https://nfc.toys/interop-ami.html
        // https://github.com/turbospok/Flipper-NTAG215-password-converter/blob/3469a58909b0d306c3dc75990c09a4c0d2bb1ff9/ntag215converter.py#L118
        private static byte[] CalculatePwd(byte[] uidBytes)
            => new byte[] { (byte)(uidBytes[1] ^ uidBytes[3] ^ 0xAA),
                            (byte)(uidBytes[2] ^ uidBytes[4] ^ 0x55),
                            (byte)(uidBytes[3] ^ uidBytes[5] ^ 0xAA),
                            (byte)(uidBytes[4] ^ uidBytes[6] ^ 0x55)};

        public static byte[] ToBin(string nfc) // This function is build so even when the file is badly formatted, it should transformable
        {
            if (!nfc.Contains($"Pages total: {TotalPages}"))
            {
                throw new ArgumentException($"Given nfc data containes more or less than {TotalPages} Pages");
            }

            var firstPageIndex = nfc.IndexOf("Page 0:");
            var fromPages = nfc.Substring(firstPageIndex);
            var bytes = new byte[NfcLength];
            for (int i = 0; i < TotalPages; i++)
            {
                var startText = $"Page {i}:";
                var startIndex = fromPages.IndexOf(startText) + startText.Length;
                var lastIndex = fromPages.IndexOf("P", startIndex + 1);
                var pageSnippet = string.Empty;
                if (lastIndex != -1)
                {
                    pageSnippet = fromPages.Substring(startIndex, lastIndex - startIndex).Trim();
                }
                else
                {
                    pageSnippet = fromPages.Substring(startIndex).Trim();
                }

                if (pageSnippet.Length > 0)
                {
                    var rawPageBytes = pageSnippet.Trim().Replace(" ", "");
                    var pageBytes = Enumerable.Range(0, rawPageBytes.Length / 2).Select(x => Convert.ToByte(rawPageBytes.Substring(x * 2, 2), 16)).ToArray();
                    pageBytes.CopyTo(bytes, i * PageDataLength);
                }
                else
                {
                    throw new ArgumentException($"Page data couldn't be parsed for page '{i}'");
                }
            }

            return bytes;
        }
    }
}
