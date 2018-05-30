using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ChattingApp.Repository.Helpers
{
    public static class ImageExtensions
    {
        public static Stream ToStream(this Image image, ImageFormat format)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (format == null) throw new ArgumentNullException(nameof(format));

            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;

            return stream;
        }
    }
}