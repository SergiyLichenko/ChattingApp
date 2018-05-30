﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using ChattingApp.Helpers.Translate.Interfaces;

namespace ChattingApp.Helpers.Translate
{
    public class YandexTranslator : ITranslator
    {
        private readonly string _yandexKey;
        private readonly WebClient _webClient;

        public YandexTranslator()
        {
            _yandexKey = ConfigurationManager.AppSettings["YandexKey"];
            _webClient = new WebClient();
        }

        public async Task<string> TranslateAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException(nameof(text));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(targetLanguage));

            try
            {
                var requestUrl = GetRequestUrl(text, targetLanguage);
                var json = await _webClient.DownloadStringTaskAsync(new Uri(requestUrl));
                var result = Json.Decode(json);

                return result.text[0];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return text;
        }

        private string GetRequestUrl(string message, string targetLanguage) =>
            "https://translate.yandex.net/api/v1.5/tr.json/translate?" +
            $"key={_yandexKey}&text={message}&lang={targetLanguage.ToLowerInvariant()}";
    }
}