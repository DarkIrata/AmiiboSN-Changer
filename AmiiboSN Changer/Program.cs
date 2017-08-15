using System;
using System.IO;
using System.Threading;

namespace AmiiboSN_Changer
{
    public class Program
    {
        private const string amiitoolFilePath = "amiitool.exe";
        private const string retailKeyFilePath = "key_retail.bin";

        private static AmiiToolHelper amiiTool = null;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                WriteError("Use 'asnc.exe \"FILE\" <AMOUNT> \"<OUTPUT>\"'.");
                WriteError("Example: 'asnc.exe \"mario.bin\" 3 \".\\newAmiibos\"'.");
                return;
            }

            if (!File.Exists(amiitoolFilePath))
            {
                WriteError($"Couldn't find '{amiitoolFilePath}' next to asnc.exe");
                return;
            }

            if (!File.Exists(retailKeyFilePath))
            {
                WriteError($"Couldn't find '{retailKeyFilePath}' next to asnc.exe");
                return;
            }

            var amiiboFilePath = args[0];
            if (!File.Exists(amiiboFilePath))
            {
                WriteError($"Couldn't find '{amiiboFilePath}'");
                return;
            }

            var amount = 1;
            if (args.Length >= 2)
            {
                var amountStr = args[1];
                if (!int.TryParse(amountStr, out amount))
                {
                    WriteError($"'{amountStr}' is not a vaild number.");
                    return;
                }
            }

            if (Environment.CurrentDirectory != AppDomain.CurrentDomain.BaseDirectory)
            {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                WriteInfo($"Changed Working Directory to '{Environment.CurrentDirectory}'");
            }

            var output = Environment.CurrentDirectory;
            if (args.Length >= 3)
            {
                output = args[2];
            }

            amiiTool = new AmiiToolHelper(amiitoolFilePath, retailKeyFilePath);
            amiiTool.AmiiToolResult += AmiiTool_AmiiToolResult;
            Execute(amiiboFilePath, amount, output);
        }

        private static void AmiiTool_AmiiToolResult(object sender, Events.AmiiToolResultEventArgs e)
        {
            if (!e.IsError)
            {
                return;
            }

            WriteError("Error while executing amiitool");
            WriteError($"Arguments used: '{e.Args}'");
            WriteError($"Error Message received: '{e.Result}'");
            Environment.Exit(1);
        }

        private static void Execute(string amiiboFilePath, int amount, string output)
        {
            WriteInfo("This tool is a C# copy of AnalogMan's Python script.");
            WriteInfo("_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            Thread.Sleep(500);

            WriteInfo($"Decrypting: {amiiboFilePath}");
            var decryptedAmiiboFilePath = amiiTool.DecryptAmiibo(amiiboFilePath);
            OneLineUp();
            WriteDone($"Decrypting: {amiiboFilePath}");

            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine();

            for (int nr = 1; nr <= amount; nr++)
            {
                try
                {
                    WriteInfo($"[Amiibo ({nr})] Generating new Serial");
                    amiiTool.GenerateSerial(out byte[] serial, out byte firstChecksum, out byte secondChecksum);
                    OneLineUp();
                    WriteDone($"[Amiibo ({nr})] Generating new Serial");

                    WriteInfo($"[Amiibo ({nr})] Writing Serial to Amiibo");
                    amiiTool.SetAmiiboSerial(decryptedAmiiboFilePath, serial, firstChecksum, secondChecksum);
                    OneLineUp();
                    WriteDone($"[Amiibo ({nr})] Writing Serial to Amiibo");

                    var newAmiiboFileName = $"{Path.GetFileNameWithoutExtension(amiiboFilePath)}_ASNC-{nr}.bin";
                    var outputPath = Path.Combine(Path.GetFullPath(output), newAmiiboFileName);
                    WriteInfo($"[Amiibo ({nr})] Encrypting: {newAmiiboFileName}");
                    amiiTool.EncryptAmiibo(decryptedAmiiboFilePath, outputPath);
                    OneLineUp();
                    WriteDone($"[Amiibo ({nr})] Encrypting: {newAmiiboFileName}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    WriteError($"Error processing for Amiibo ({nr})");
                    WriteError(ex.Message);
                }
            }

            WriteInfo("Cleaning up");
            amiiTool.CleanUp(amiiboFilePath, true);
            OneLineUp();
            WriteDone("Cleaning up");
        }

        private static void OneLineUp()
        {
            var lastLine = Console.CursorTop - 1;
            if (lastLine < 0)
            {
                lastLine = 0;
            }

            Console.SetCursorPosition(0, lastLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, lastLine);
        }

        private static void WriteError(string text, params object[] args)
        {
            WritePrefixed(ConsoleColor.Red, "ERROR", text, args);
        }

        private static void WriteInfo(string text, params object[] args)
        {
            WritePrefixed(ConsoleColor.Cyan, "INFO", text, args);
        }

        private static void WriteDone(string text, params object[] args)
        {
            WritePrefixed(ConsoleColor.Green, "DONE", text, args);
        }

        private static void WritePrefixed(ConsoleColor color, string prefix, string text, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[");
            Console.ForegroundColor = color;
            Console.Write(prefix);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"] {string.Format(text, args)}");
        }
    }
}
