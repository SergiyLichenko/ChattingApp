using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IAuthContext _authContext;

        public LanguageRepository(IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
        }

        public async Task<Language> GetByLanguageTypeAsync(LanguageType languageType) =>
            await _authContext.Languages.FirstOrDefaultAsync(x => x.LanguageType == languageType);
    }
}