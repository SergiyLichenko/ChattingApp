using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;
using PasswordHasher = ChattingApp.Repository.Helpers.PasswordHasher;

namespace ChattingApp.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IAuthContext _authContext;

        public UserRepository(IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            return await _authContext.Users.Include(x => x.Chats)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(UserDomain user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Password != user.ConfirmPassword)
                throw new InvalidOperationException("Password and confirmation password do not match");

            if (string.IsNullOrEmpty(user.Img))
                user.Img = ImageReader.GetDefaultImage();

            var newUser = new ApplicationUser
            {
                Password = PasswordHasher.HashPassword(user.Password),
                Email = user.Email,
                UserName = user.UserName,
                Img = user.Img
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

            existingUser.Img = user.Img;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingUser.Password = PasswordHasher.HashPassword(user.Password);

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