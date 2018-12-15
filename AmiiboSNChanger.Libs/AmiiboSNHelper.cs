using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using LibAmiibo.Data;

namespace AmiiboSNChanger.Libs
{
    public class AmiiboSNHelper
    {
        public const string FileNameExtension = "_ASNC-";
        public const string FileType = ".bin";

        private static RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

        public static AmiiboTag DecryptNtag(string amiiboPath)
        {
            if (!File.Exists(amiiboPath))
            {
                Console.WriteLine($"{amiiboPath} not found!");
                return null;
            }

            byte[] encryptedNtagData = File.ReadAllBytes(amiiboPath);
            try
            {
                return AmiiboTag.DecryptWithKeys(encryptedNtagData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while decrypting {amiiboPath}");
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static void EncryptNtag(string savePath, AmiiboTag amiibo)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                Console.WriteLine("Missing save path");
                return;
            }

            if (amiibo == null)
            {
                Console.WriteLine("Can't encrypt amiibo if no amiibo was given, DUH!");
                return;
            }

            try
            {
                byte[] encryptedNtagData = amiibo.EncryptWithKeys();
                File.WriteAllBytes(savePath, encryptedNtagData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while encrypting {amiibo.Amiibo?.RetailName}");
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
    }
}
