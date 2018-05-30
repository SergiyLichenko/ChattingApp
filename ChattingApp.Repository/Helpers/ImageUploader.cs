﻿using System;
using System.Configuration;
using System.Drawing;
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

        public static async Task<string> UploadAsync(Image image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            using (var imageStream = image.ToStream(image.RawFormat))
            {
                var uploadParams = new ImageUploadParams()
                { File = new FileDescription(Guid.NewGuid().ToString(), imageStream) };

                var response = await Cloudinary.UploadAsync(uploadParams);
                return response.Uri.ToString();
            }
        }

        public static async Task<string> UploadAsync(string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image)) throw new ArgumentNullException(nameof(base64Image));

            return await UploadAsync(ImageHelper.Base64ToImage(base64Image));
        }
    }
}