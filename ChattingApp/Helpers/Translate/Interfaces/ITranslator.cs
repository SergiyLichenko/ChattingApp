using System.Threading.Tasks;

namespace ChattingApp.Helpers.Translate.Interfaces
{
    public interface ITranslator
    {
        Task<string> TranslateAsync(string message, string targetLanguage);
    }
}