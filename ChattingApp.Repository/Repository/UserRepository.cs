using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private IAuthContext _authContext;

        public UserRepository(IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _userManager = new UserManager<ApplicationUser>(new ApplicationUserStore((AuthContext)authContext));
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(nameof(id));
            
            return await _userManager.FindByIdAsync(id);
        }











        public ApplicationUser Remove(ApplicationUser instance)
        {
            try
            {
                _userManager.Delete(instance);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public ApplicationUser UpdateAsync(ApplicationUser instance)
        {
            try
            {
                _userManager.Update(instance);
                return instance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddImage(string userId, byte[] imageByteArray)
        {
            try
            {

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public ApplicationUser GetUserByName(string userName)
        {
            ApplicationUser user = _userManager.FindByName(userName);
            return user;
        }

        public void Add(ApplicationUser instance)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = instance.UserName,
                Email = instance.Email,
                Img = instance.Img
            };
            _userManager.Create(user, instance.PasswordHash);
        }

        public bool AddUserToChat(string username, string chatId)
        {
            try
            {
                var chat = _authContext.Chats.FirstOrDefault(x => x.Id.Equals(new Guid(chatId)));
                //var selectedUser = _authContext.Users.Include("Chats").FirstOrDefault(x => x.UserName.Equals(username));
                //selectedUser.Chats.Add(chat);

                //int count = _authContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ChangePassword(string username, string currentPassword, string newPassword)
        {
            try
            {
                var user = _userManager.FindByName(username);
                var result = _userManager.ChangePassword(user.Id, currentPassword, newPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ApplicationUser ChangeEmail(string username, string newEmail)
        {
            try
            {
                var user = _userManager.FindByName(username);
                user.Email = newEmail;
                _userManager.Update(user);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ApplicationUser ChangeUsername(string oldUsername, string newUsername)
        {
            try
            {
                var user = _userManager.FindByName(oldUsername);
                user.UserName = newUsername;
                _userManager.Update(user);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ApplicationUser ChangeImage(string oldUsername, string img)
        {
            try
            {
                var user = _userManager.FindByName(oldUsername);
                user.Img = img;
                _userManager.Update(user);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException(nameof(password));

            return await _userManager.FindAsync(userName, password);
        }
    }
}
