using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Smart.Data;
using Smart.Entities;
using Smart.Models.Contexts;
using Smart.Models.Entities;

namespace Smart.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private AuthContext _ctx;

        public UserRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));

        }

        public int Count => _ctx.Users.Count();

        public ApplicationUser Get(string id)
        {
            return _userManager.FindById(id);
        }

        public bool Remove(ApplicationUser instance)
        {
            try
            {
                _userManager.Delete(instance);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ApplicationUser Update(ApplicationUser instance)
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

        public ApplicationUser GetByUserNameAndPassword(string userName, string password)
        {
            ApplicationUser user = _userManager.Find(userName, password);
            return user;
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

        public IdentityResult Add(ApplicationUser instance)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = instance.UserName,
                Email = instance.Email,
                Img = instance.Img
            };
            IdentityResult result = _userManager.Create(user, instance.PasswordHash);
            string id = _userManager.Find(instance.UserName, instance.PasswordHash).Id;
            _userManager.AddToRole(id, "User");
            return result;

        }

        public bool AddUserToChat(string username, string chatId)
        {
            try
            {
                var chat = _ctx.Chats.FirstOrDefault(x => x.Id.Equals(new Guid(chatId)));
                var selectedUser =  _ctx.Users.Include("Chats").FirstOrDefault(x => x.UserName.Equals(username));
                selectedUser.Chats.Add(chat);

                int count = _ctx.SaveChanges();
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
        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public List<Chat> GetUserChats(ApplicationUser user)
        {
            List<Chat> result = new List<Chat>();
            return result;
        } 

        public void Dispose()
        {
            _userManager.Dispose();
            _ctx.Dispose();

        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken =
                _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId)
                    .SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        ApplicationUser IRepository<ApplicationUser>.Remove(ApplicationUser instance)
        {
            throw new NotImplementedException();
        }
    }
}
