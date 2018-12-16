using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ASNC_GUI.Properties;
using LibAmiibo.Data;

namespace ASNC_GUI
{
    public class AmiiboTagWrapper
    {
        public AmiiboTag Amiibo { get; private set; }

        public string Name => $"{this.Amiibo?.Amiibo?.CharacterName} ({this.Amiibo?.Amiibo?.AmiiboSetName})";

        public Image IsDecrypted => (this.Amiibo?.IsDecrypted ?? false) ? Resources.unlocked : Resources.locked;

        public Image HasUserData => (this.Amiibo?.HasUserData ?? false) ? Resources.id_card : Resources.Empty;

        public Image HasAppData => (this.Amiibo?.HasAppData ?? false) ? Resources.database : Resources.Empty;

        public Image AmiiboImage => this.GetBitmap(40, 40);

        public AmiiboTagWrapper(AmiiboTag amiibo)
        {
            this.Amiibo = amiibo;
        }

        // https://github.com/rds1983/StbSharp/wiki/stb_image.h
        public Bitmap GetBitmap(int width, int height)
        {
            var resizedImg = this.Amiibo?.Amiibo?.AmiiboImage.CreateResized(width, height);
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
    }
}
