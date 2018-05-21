using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Repository.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly IAuthContext _authContext;
        private readonly IUserRepository _userRepository;

        public ChatRepository(IAuthContext authContext, IUserRepository userRepository)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<Chat>> GetAllAsync() =>
            await _authContext.Chats.Include(x => x.Users).ToListAsync();

        public async Task<Chat> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            return await _authContext.Chats.Include(x => x.Users)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            var authorId = HttpContext.Current.User.Identity.GetUserId();
            var author = await _userRepository.GetByIdAsync(authorId);

            chat.AuthorId = authorId;
            chat.Users = new List<ApplicationUser>() { author };
            chat.CreateDate = DateTime.Now;

            _authContext.Chats.Add(chat);
            await _authContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            var chatEntity = await GetByIdAsync(chat.Id);
            if (chatEntity == null) throw new InvalidOperationException();

            var deletedUsers = chatEntity.Users
                .Except(chat.Users, user => user.Id).ToList();
            var addedUsers = chat.Users.Except(chatEntity.Users, x => x.Id).ToList();

            _authContext.Entry(chatEntity).CurrentValues.SetValues(chat);

            foreach (var user in deletedUsers)
                user.Chats.Remove(chatEntity);

            foreach (var user in addedUsers)
            {
                var existingUser = await _userRepository.GetByIdAsync(user.Id);
                existingUser.Chats.Add(chatEntity);
            }

            await _authContext.SaveChangesAsync();
        }











        public Chat Remove(Chat instance)
        {
            try
            {
                var toDelete = _authContext.Chats.Include("Users").
                       SingleOrDefault(x => x.Id.Equals(instance.Id));
                var deleted = _authContext.Chats.Remove(toDelete);

                _authContext.SaveChangesAsync();
                return deleted;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public Chat UpdateTitle(Chat instance)
        {
            try
            {
                var chat = _authContext.Chats.Single(x => x.Id == instance.Id);
                chat.Title = instance.Title;
                _authContext.SaveChangesAsync();

                return chat;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Chat UpdateImage(Chat instance)
        {
            try
            {
                var chat = _authContext.Chats.Single(x => x.Id == instance.Id);
                chat.Img = instance.Img;
                _authContext.SaveChangesAsync();

                return chat;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetAllUsersCountForChat(int id)
        {
            try
            {
                var chat = _authContext.Chats.Include("Users").Single(x => x.Id == id);
                if (chat == null) return 0;
                return chat.Users.Count();
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public List<ApplicationUser> GetAllUsersForChat(string id)
        {
            try
            {
                var chat = _authContext.Chats.Include("Users").Single(x => x.Id.ToString() == id);

                return chat?.Users.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }





        public bool Quit(string chatId, string username)
        {
            try
            {
                //var user = _authContext.Users.Single(x => x.UserName == username);
                //var chat = _authContext.Chats.Include("Users").Single(x => x.Id == new Guid(chatId));
                //chat.Users.Remove(user);
                //_authContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<Chat> GetAllChatsByUsername(string userName)
        {
            var chats = _authContext.Chats.Include("Users").Where(x =>
            x.Users.Count(y => y.UserName.Equals(userName)) > 0).ToList();
            return chats;
        }


    }
}
