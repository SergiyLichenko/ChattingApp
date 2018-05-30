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
        private const LanguageType DefaultLanguageType = LanguageType.En;
        private readonly IAuthContext _authContext;

        public LanguageRepository(IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
        }

        public async Task<Language> GetDefaultAsync() =>
            await _authContext.Languages.FirstOrDefaultAsync(x => x.LanguageType == DefaultLanguageType);

        public async Task<Language> GetByIdAsync(int id)
        {
            if(id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            return await _authContext.Languages.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}