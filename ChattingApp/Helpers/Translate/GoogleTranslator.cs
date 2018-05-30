﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Helpers.Translate.Interfaces;
using Google.Apis.Services;
using Google.Apis.Translate.v2;

namespace ChattingApp.Helpers.Translate
{
    public class GoogleTranslator : ITranslator
    {
        public async Task<string> TranslateAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException(nameof(text));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(targetLanguage));

            var translateService = new TranslateService(
                new BaseClientService.Initializer() { ApiKey = "AIzaSyCQ6GuhRA5zl_aJ245sAoiBie4UBjBD4sc" });
            var response = await translateService.Translations.List(text, targetLanguage).ExecuteAsync();

            return response.Translations.First().TranslatedText;
        }
    }
}