using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Autofac.Features.Indexed;
using ChattingApp.Helpers.Translate.Interfaces;
using ChattingApp.Models;
using ChattingApp.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Helpers.Translate
{
    public class MessageTranslator : IMessageTranslator
    {
        private readonly ITranslator _googleTranslator;
        private readonly ITranslator _bingTranslator;
        private readonly ITranslator _yandexTranslator;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageTranslator(IIndex<string, ITranslator> translators,
            IMessageRepository messageRepository,
            IUserRepository userRepository)
        {
            _googleTranslator = translators[TranslationSource.Google.ToString()] ?? throw new ArgumentNullException(nameof(translators));
            _bingTranslator = translators[TranslationSource.Bing.ToString()] ?? throw new ArgumentNullException(nameof(translators));
            _yandexTranslator = translators[TranslationSource.Yandex.ToString()] ?? throw new ArgumentNullException(nameof(translators));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IDictionary<TranslationSource, string>> TranslateAsync(int messageId)
        {
            if (messageId < 0) throw new ArgumentOutOfRangeException(nameof(messageId));

            var message = await _messageRepository.GetByIdAsync(messageId);
            var userId = Convert.ToInt32(HttpContext.Current.User.Identity.GetUserId());
            var currentUser = await _userRepository.GetByIdAsync(userId);
            var targetLanguage = currentUser.Language.LanguageType.ToString();

            var googleTranslateTask = _googleTranslator.TranslateAsync(message.Text, targetLanguage);
            var bingTranslateTask = _bingTranslator.TranslateAsync(message.Text, targetLanguage);
            var yandexTranslateTask = _yandexTranslator.TranslateAsync(message.Text, targetLanguage);

            await Task.WhenAll(googleTranslateTask, bingTranslateTask, yandexTranslateTask);

            return new Dictionary<TranslationSource, string>()
            {
                {TranslationSource.Google, await googleTranslateTask},
                {TranslationSource.Bing, await bingTranslateTask},
                {TranslationSource.Yandex, await yandexTranslateTask}
            };
        }
    }
}