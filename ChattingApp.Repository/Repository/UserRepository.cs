using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(IAuthContext authContext)
        {
            _userManager = new UserManager<ApplicationUser>(new ApplicationUserStore((AuthContext)authContext));
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(nameof(id));

            return await _userManager.FindByIdAsync(id);
        }

        public async Task AddAsync(UserDomain user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Password != user.ConfirmPassword)
                throw new InvalidOperationException("Password and confirmation password do not match");

            if (string.IsNullOrEmpty(user.Img))
                user.Img = GetDefaultImage();

            await _userManager.CreateAsync(new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                Img = user.Img
            }, user.Password);
        }

        public async Task UpdateAsync(UserDomain user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Password != user.ConfirmPassword)
                throw new InvalidOperationException("Password and confirmation password do not match");

            var identityResult = await _userManager.ChangePasswordAsync(user.Id, user.OldPassword, user.Password);
            if (identityResult.Errors.Any())
                throw new ArgumentException(string.Join(Environment.NewLine, identityResult.Errors));

            var existingUser = await _userManager.FindByIdAsync(user.Id);
            existingUser.Img = user.Img;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            await _userManager.UpdateAsync(existingUser);
        }

        private string GetDefaultImage()
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/images/default.png");
            var image = Image.FromFile(path);
            var imageString = ImageResizer.ImageToBase64(image, ImageFormat.Png);
            return imageString.Insert(0, "data:image/png;base64,");
        }

        public async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException(nameof(password));

            return await _userManager.FindAsync(userName, password);
        }
    }
}