using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Language> GetDefaultAsync();
        Task<Language> GetByIdAsync(int id);
    }
}