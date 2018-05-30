using System.Threading.Tasks;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Language> GetDefaultAsync();
        Task<Language> GetByLanguageTypeAsync(LanguageType languageType);
    }
}