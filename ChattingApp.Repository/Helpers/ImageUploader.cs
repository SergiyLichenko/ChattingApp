using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ChattingApp.Repository.Helpers
{
    public class ImageUploader
    {
        private static readonly Cloudinary Cloudinary;

        static ImageUploader()
        {
            var account = new Account(
                ConfigurationManager.AppSettings["CloudinaryCloudName"],
                ConfigurationManager.AppSettings["CloudinaryApiKey"],
                ConfigurationManager.AppSettings["CloudinarySecretKey"]);

            Cloudinary = new Cloudinary(account);
        }

        public static async Task<string> UploadAsync(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var uploadParams = new ImageUploadParams
                { File = new FileDescription(Guid.NewGuid().ToString(), stream) };

            var response = await Cloudinary.UploadAsync(uploadParams);
            return response.Uri.ToString();
        }

        public static async Task<string> UploadAsync(string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image)) throw new ArgumentNullException(nameof(base64Image));

            var bytes = Convert.FromBase64String(base64Image.RemoveBase64Header());
            using (var memoryStream = new MemoryStream(bytes))
                return await UploadAsync(memoryStream);
        }
    }
}