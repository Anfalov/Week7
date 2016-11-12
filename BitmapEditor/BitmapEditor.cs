using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BitmapEditor
{
    public class BitmapEditor: IDisposable
    {
        
        private Bitmap bitmap;
        private BitmapData bitmapData;
        private bool isDisposed = false;
        private byte[] pixels;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public BitmapEditor(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            Width = bitmap.Width;
            Height = bitmap.Height;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            pixels = new byte[Width * Height * 3];
            Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (isDisposed)
                throw new InvalidOperationException("This BitmapEditor is disposed");
            int i = ((y * Width) + x) * 3;
            pixels[i] = b;
            pixels[i + 1] = g;
            pixels[i + 2] = r;
        }

        ~BitmapEditor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UnlockBits()
        {
            Dispose();
        }

        protected virtual void Dispose(bool fromDisposeMethod)
        {
            if (!isDisposed)
            {
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                bitmap.UnlockBits(bitmapData);
                isDisposed = true;
            }
        }
    }
}
