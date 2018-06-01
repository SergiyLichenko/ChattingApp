using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using PasswordHasher = ChattingApp.Repository.Helpers.PasswordHasher;

namespace ChattingApp.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IAuthContext _authContext;
        private readonly ILanguageRepository _languageRepository;

        public UserRepository(IAuthContext authContext,
            ILanguageRepository languageRepository)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            return await _authContext.Users.Include(x => x.Chats)
                .Include(x => x.Language)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() => 
            await _authContext.Users.ToListAsync();

        public async Task AddAsync(UserDomain user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Password != user.ConfirmPassword)
                throw new InvalidOperationException("Password and confirmation password do not match");

            if (string.IsNullOrEmpty(user.Img))
                user.Img = ConfigurationManager.AppSettings["DefaultImageUrl"];

            var newUser = new ApplicationUser
            {
                Password = PasswordHasher.HashPassword(user.Password),
                Email = user.Email,
                UserName = user.UserName,
                Img = user.Img,
                Language = await _languageRepository.GetByLanguageTypeAsync(user.Language.LanguageType)
            };
            _authContext.Users.Add(newUser);
            await _authContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserDomain user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Password != user.ConfirmPassword)
                throw new InvalidOperationException("Password and confirmation password do not match");

            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null) throw new InvalidOperationException("user is not found");

            var isVerifiedPassword = PasswordHasher
                .VerifyHashedPassword(existingUser.Password, user.OldPassword);

            if (!isVerifiedPassword)
                throw new InvalidOperationException("Incorrect password");

            if (user.Img != null && !user.Img.IsUrl())
                existingUser.Img = await ImageUploader.UploadAsync(user.Img);

            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingUser.Password = PasswordHasher.HashPassword(user.Password);
            existingUser.Language = await _languageRepository.GetByLanguageTypeAsync(user.Language.LanguageType);

            await _authContext.SaveChangesAsync();
        }

        public async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException(nameof(password));

            var users = await _authContext.Users.Where(x => x.UserName == userName).ToListAsync();
            return users.FirstOrDefault(x => PasswordHasher.VerifyHashedPassword(x.Password, password));
        }
    }
}