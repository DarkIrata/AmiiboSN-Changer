using System;
using System.Diagnostics;
using System.IO;
using AmiiboSN_Changer.Events;

namespace AmiiboSN_Changer
{
    public class AmiiToolHelper
    {
        private const string DecryptedExtension = "decrypted";
        private const string BackupExtension = "asncBAK";

        private readonly string amiitoolFilePath = string.Empty;
        private readonly string retailKeyFilePath = string.Empty;

        public event EventHandler<AmiiToolResultEventArgs> AmiiToolResult;

        public AmiiToolHelper(string amiitoolFilePath, string retailKeyFilePath)
        {
            this.amiitoolFilePath = amiitoolFilePath;
            this.retailKeyFilePath = retailKeyFilePath;
        }

        public void CleanUp(string filePath, bool restoreAvailableBackup = false)
        {
            var decryptedFilePath = $"{filePath}.{DecryptedExtension}";
            if (File.Exists(decryptedFilePath))
            {
                File.Delete(decryptedFilePath);
            }

            var bakFilePath = $"{filePath}.{BackupExtension}";
            if (File.Exists(bakFilePath))
            {
                if (restoreAvailableBackup)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.Move(bakFilePath, filePath);
                }

                File.Delete(bakFilePath);
            }
        }

        public string DecryptAmiibo(string filePath)
        {
            var decryptedFilePath = $"{filePath}.{DecryptedExtension}";
            var bakFilePath = $"{filePath}.{BackupExtension}";
            this.CleanUp(filePath);
            File.Copy(filePath, bakFilePath);

            ExecuteAmiiTool($"-d -k {this.retailKeyFilePath} -i \"{filePath}\" -o \"{decryptedFilePath}\"");
            return decryptedFilePath;
        }

        public void EncryptAmiibo(string filePath, string saveFilePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (saveFilePath == null)
            {
                throw new ArgumentNullException(nameof(saveFilePath));
            }

            var savePath = Path.GetDirectoryName(saveFilePath);
            Directory.CreateDirectory(savePath);

            ExecuteAmiiTool($"-e -k {this.retailKeyFilePath} -i \"{filePath}\" -o \"{saveFilePath}\"");
        }

        public void GenerateSerial(out byte[] serial, out byte firstChecksum, out byte secondCecksum)
        {
            serial = new byte[7];
            var r = new Random();
            for (int i = 0; i < serial.Length; i++)
            {
                serial[i] = (byte)r.Next(0, 255);
            }

            firstChecksum = (byte)(serial[0] ^ serial[1] ^ serial[2]);
            secondCecksum = (byte)(serial[3] ^ serial[4] ^ serial[5] ^ serial[6]);
        }

        public void SetAmiiboSerial(string filePath, byte[] serial, byte firstChecksum, byte secondCecksum)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                stream.Position = 0;
                stream.Write(new[] { secondCecksum }, 0, 1);

                stream.Position = 0x1D4;
                var newSerial = new byte[8];

                newSerial[0] = firstChecksum;
                Array.Copy(serial, 0, newSerial, 1, serial.Length);
                stream.Write(newSerial, 0, newSerial.Length);
            }
        }

        public void ExecuteAmiiTool(string args)
        {
            var amiiToolProc = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = this.amiitoolFilePath,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            amiiToolProc.Start();
            amiiToolProc.WaitForExit();

            var eventArgs = new AmiiToolResultEventArgs()
            {
                Args = args
            };

            eventArgs.Result = amiiToolProc.StandardOutput.ReadToEnd();
            var errorResult = amiiToolProc.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(errorResult))
            {
                eventArgs.Result = errorResult;
                eventArgs.IsError = true;
            }

            this.AmiiToolResult?.Invoke(this, eventArgs);
        }
    }
}
