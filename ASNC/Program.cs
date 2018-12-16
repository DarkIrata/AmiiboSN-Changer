using System;
using System.Diagnostics;
using System.IO;
using AmiiboSNChanger.Libs;
using LibAmiibo.Data;
using PowerArgs;

namespace ASNC
{
    public class Program
    {
        public static void Main(string[] rawArgs)
        {
            ASNCArgs args = null;
            try
            {
                args = Args.Parse<ASNCArgs>(rawArgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<ASNCArgs>());
                Environment.Exit(1);
            }

            var sw = new Stopwatch();
            Console.WriteLine("### Amiibo Serial Number Changer ###");

            var amiibo = GetDecryptedAmiibo(args.Amiibo);
            GenerateNewAmiiboFiles(amiibo, args.Amount, args.Amiibo, args.Output);

            Console.WriteLine("### FINISHED ###");
            Console.ReadLine();
        }

        private static AmiiboTag GetDecryptedAmiibo(string amiiboPath)
        {
            Console.WriteLine("Decrypting Amiibo");
            var amiibo = AmiiboSNHelper.LoadAndDecryptNtag(amiiboPath);
            if (amiibo == null || !amiibo.IsDecrypted)
            {
                Console.WriteLine("Amiibo was not decrypted");
                Console.WriteLine("Check if you are missing the key_retail.bin");
                Environment.Exit(3);
            }

            return amiibo;
        }

        private static void GenerateNewAmiiboFiles(AmiiboTag amiibo, uint amount, string amiiboPath, string output)
        {
            Console.WriteLine("Generating and saving modified Amiibos");
            for (int i = 0; i < amount; i++)
            {
                var fileName = Path.GetFileNameWithoutExtension(amiiboPath) + AmiiboSNHelper.FileNameExtension + i + AmiiboSNHelper.FileType;
                var savePath = Path.Combine(output, fileName);

                Console.WriteLine($">> Creating Amiibo {savePath}");
                amiibo.RandomizeUID();

                AmiiboSNHelper.EncryptNtag(savePath, amiibo);
            }
        }
    }
}
