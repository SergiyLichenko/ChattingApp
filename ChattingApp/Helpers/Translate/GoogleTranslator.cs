﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Helpers.Translate.Interfaces;
using Google.Apis.Services;
using Google.Apis.Translate.v2;

namespace ChattingApp.Helpers.Translate
{
    public class GoogleTranslator : ITranslator
    {
        private readonly TranslateService _translateService;

        public GoogleTranslator()
        {
            _translateService = new TranslateService(new BaseClientService.Initializer()
            { ApiKey = ConfigurationManager.AppSettings["GoogleApiKey"] });
        }

        public async Task<string> TranslateAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException(nameof(text));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(targetLanguage));

            try
            {
                var response = await _translateService.Translations.List(text, targetLanguage).ExecuteAsync();
                return response.Translations.First().TranslatedText;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return text;
        }
    }
}