using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ChattingApp.Repository.Helpers
{
    public static class ImageHelper
    {
        private const string ImageHeader = "data:image/png;base64,";

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

        public static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) throw new ArgumentException(nameof(base64String));

            if (base64String.Contains(ImageHeader))
                base64String = base64String.Remove(0, ImageHeader.Length);

            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                return Image.FromStream(ms, true);
        }
    }
}