
using Android.Graphics;
using System;
using System.IO;

namespace iDigIt.Helpers
{
    public static class ImageResizer
    {
        static ImageResizer()
        {
        }

        public static byte[] Resize(byte[] imageData, double resizeFactor)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, Convert.ToInt32(originalImage.Width * resizeFactor), Convert.ToInt32(originalImage.Height * resizeFactor), false);
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}
