using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using ASNC_GUI.Properties;
using LibAmiibo.Data;

namespace ASNC_GUI
{
    public class AmiiboTagWrapper
    {
        public string FilePath { get; private set; }

        public AmiiboTag AmiiboTag { get; private set; }

        public string Name => $"{this.AmiiboTag?.Amiibo?.RetailName} ({this.AmiiboTag?.Amiibo?.AmiiboSetName})";

        public Image IsDecrypted => (this.AmiiboTag?.IsDecrypted ?? false) ? Resources.unlocked : Resources.locked;

        public Image HasUserData => (this.AmiiboTag?.HasUserData ?? false) ? Resources.id_card : Resources.Empty;

        public Image HasAppData => (this.AmiiboTag?.HasAppData ?? false) ? Resources.database : Resources.Empty;

        public Image AmiiboImage => this.GetBitmap(40, 40);

        public AmiiboTagWrapper(string filePath, AmiiboTag amiibo)
        {
            this.FilePath = filePath;
            this.AmiiboTag = amiibo;
        }

        // https://github.com/rds1983/StbSharp/wiki/stb_image.h
        public Bitmap GetBitmap(int width, int height)
        {
            var resizedImg = this.AmiiboTag?.Amiibo?.AmiiboImage.CreateResized(width, height);
            var data = resizedImg.Data;

            // Convert rgba to bgra
            for (int i = 0; i < width * height; ++i)
            {
                byte r = data[i * 4];
                byte g = data[i * 4 + 1];
                byte b = data[i * 4 + 2];
                byte a = data[i * 4 + 3];


                data[i * 4] = b;
                data[i * 4 + 1] = g;
                data[i * 4 + 2] = r;
                data[i * 4 + 3] = a;
            }

            // Create Bitmap
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);

            Marshal.Copy(data, 0, bmpData.Scan0, bmpData.Stride * bmp.Height);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        // Since the Amiibo was already loaded once, we can reload it instantly. If it fails. the user fucked someting up ¯\_(ツ)_/¯
        public void ReloadAmiibo() => this.AmiiboTag = AmiiboTag.DecryptWithKeys(File.ReadAllBytes(this.FilePath));
    }
}
