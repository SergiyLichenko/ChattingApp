using System;
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
                Name = "Doc_Transl_demo",
                SubscriptionKey = "48916fdea51c45fd8088a34eee7047a0",
                SubscriptionKeyAlternate = "84bbc7b1419046b8a7bed2e4559232fb"
            });
        }

        public async Task<string> TranslateAsync(string message, string targetLanguage)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException(nameof(message));
            if (string.IsNullOrEmpty(targetLanguage)) throw new ArgumentException(nameof(message));

            var requestParameter = new RequestParameter() {To = new[] {targetLanguage}};
            var result = await _translateClient.TranslateAsync(
                new RequestContent(message), requestParameter);

            return result.First().Translations.First().Text;
        }
    }
}