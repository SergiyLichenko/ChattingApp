using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using ChattingApp.Helpers.Translate.Interfaces;

namespace ChattingApp.Helpers.Translate
{
    public class YandexTranslator : ITranslator
    {
        private readonly string _yandexKey;

        public YandexTranslator()
        {
            _yandexKey = ConfigurationManager.AppSettings["YandexKey"];
        }

        public async Task<string> TranslateAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException(nameof(text));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(targetLanguage));

            try
            {
                var requestUrl = GetRequestUrl(text, targetLanguage);
                var json = await GetAsync(requestUrl);
                var result = Json.Decode(json);

                return result.text[0];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return text;
        }

        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private string GetRequestUrl(string message, string targetLanguage) =>
            "https://translate.yandex.net/api/v1.5/tr.json/translate?" +
            $"key={_yandexKey}&text={Uri.EscapeDataString(message)}" +
            $"&lang={targetLanguage.ToLowerInvariant()}";
    }
}