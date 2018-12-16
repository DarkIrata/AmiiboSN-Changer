using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using AmiiboSNChanger.Libs.Events;
using LibAmiibo.Data;

namespace AmiiboSNChanger.Libs
{
    public class AmiiboSNHelper
    {
        public const string FileNameExtension = "_ASNC-";
        public const string FileType = ".bin";

        public static event EventHandler<ActionOutputEventArgs> ActionOutput;

        private static RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

        public static AmiiboTag LoadAndDecryptNtag(string amiiboPath)
        {
            var amiiboFileNotFound = $"{amiiboPath} not found!";

            if (!File.Exists(amiiboPath))
            {
                Console.WriteLine(amiiboFileNotFound);
                ActionOutput?.Invoke(null, new ActionOutputEventArgs(false, amiiboFileNotFound));
                return null;
            }

            byte[] encryptedNtagData = File.ReadAllBytes(amiiboPath);
            try
            {
                return AmiiboTag.DecryptWithKeys(encryptedNtagData);
            }
            catch (Exception ex)
            {
                string errorDecrypting = $"Error while decrypting {amiiboPath}";

                ActionOutput?.Invoke(null, new ActionOutputEventArgs(false, errorDecrypting + Environment.NewLine + Environment.NewLine + ex.Message));
                Console.WriteLine(errorDecrypting);
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static void EncryptNtag(string savePath, AmiiboTag amiibo)
        {
            const string missingSavePath = "Missing save path";
            const string missingAmibo = "Can't encrypt amiibo if no amiibo was given, DUH!";

            if (string.IsNullOrEmpty(savePath))
            {
                Console.WriteLine(missingSavePath);
                ActionOutput?.Invoke(null, new ActionOutputEventArgs(false, missingSavePath));
                return;
            }

            if (amiibo == null)
            {
                Console.WriteLine(missingAmibo);
                ActionOutput?.Invoke(null, new ActionOutputEventArgs(false, missingAmibo));
                return;
            }

            try
            {
                byte[] encryptedNtagData = amiibo.EncryptWithKeys();
                File.WriteAllBytes(savePath, encryptedNtagData);
            }
            catch (Exception ex)
            {
                string errorEncrypting = $"Error while encrypting {amiibo.Amiibo?.AmiiboSetName}";

                ActionOutput?.Invoke(null, new ActionOutputEventArgs(false, errorEncrypting + Environment.NewLine + Environment.NewLine + ex.Message));
                Console.WriteLine(errorEncrypting);
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetBytesAsString(byte[] bytes)
        {
            var result = string.Empty;
            foreach (var item in bytes)
            {
                result += item.ToString("X2") + " ";
            }

            return result.Trim();
        }

        public static byte[] CustomRandomizeSerial()
        {
            var newUID = new byte[7];
            rngCryptoServiceProvider.GetBytes(newUID);
            newUID[0] = 4;
            if (newUID[4] == 136)
            {
                newUID[4]++;
            }

            return newUID;
        }

        public static bool EqualAmiiboTag(AmiiboTag at1, AmiiboTag at2) =>
                StructuralComparisons.StructuralEqualityComparer.Equals(at1.UID, at2.UID) &&
                at1.HasAppData == at2.HasAppData &&
                at1.HasUserData == at2.HasUserData &&
                at1.WriteCounter == at2.WriteCounter;
    }
}
