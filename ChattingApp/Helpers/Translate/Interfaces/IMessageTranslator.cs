using System.Collections.Generic;
using System.Threading.Tasks;
using ChattingApp.Models;

namespace ChattingApp.Helpers.Translate.Interfaces
{
    public interface IMessageTranslator
    {
        Task<IDictionary<TranslationSource, string>> TranslateAsync(int messageId);
    }
}