using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace ChattingApp.Repository.Helpers
{
    public static class ImageReader
    {
        public static string GetDefaultImage()
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/images/default.png");
            var image = Image.FromFile(path);
            var imageString = ImageResizer.ImageToBase64(image, ImageFormat.Png);
            return imageString.Insert(0, "data:image/png;base64,");
        }
    }
}