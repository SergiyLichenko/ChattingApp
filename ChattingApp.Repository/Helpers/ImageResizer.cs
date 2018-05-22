using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace ChattingApp.Repository.Helpers
{
    public static class ImageResizer
    {
        public static string ProcessImage(string img, int size)
        {
            if (string.IsNullOrEmpty(img)) throw new ArgumentException(nameof(img));
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

            var base64Str = Regex.Matches(img, @"base64,(?<base>\S+)")[0].Groups["base"].Value;

            var bytes = Convert.FromBase64String(base64Str);
            using (var ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                var resized = ResizeImage(size, size, image);

                return ImageToBase64(resized, ImageFormat.Jpeg);
            }
        }

        public static string ImageToBase64(Image image, ImageFormat format)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (format == null) throw new ArgumentNullException(nameof(format));

            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
        }

        private static Image ResizeImage(int newWidth, int newHeight, Image imgPhoto)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent ;

            var nPercentW = newWidth / (float)sourceWidth;
            var nPercentH = newHeight / (float)sourceHeight;
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }
    }
}
