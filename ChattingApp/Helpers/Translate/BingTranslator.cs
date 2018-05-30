using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Helpers.Translate.Interfaces;
using CognitiveServices.Translator;
using CognitiveServices.Translator.Configuration;
using CognitiveServices.Translator.Translate;

namespace ChattingApp.Helpers.Translate
{
    public class BingTranslator : ITranslator
    {
        private readonly ITranslateClient _translateClient;

        public BingTranslator()
        {
            _translateClient = new TranslateClient(new CognitiveServicesConfig()
            {
                SubscriptionKey = ConfigurationManager.AppSettings["BingKey"],
                SubscriptionKeyAlternate = ConfigurationManager.AppSettings["BingAlternateKey"]
            });
        }

        public async Task<string> TranslateAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException(nameof(text));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(text));

            try
            {
                var requestParameter = new RequestParameter() { To = new[] { targetLanguage } };
                var result = await _translateClient.TranslateAsync(
                    new RequestContent(text), requestParameter);

                return result.First().Translations.First().Text;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return text;
        }
    }
}