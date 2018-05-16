using System;
using System.Collections.Generic;

namespace Smart.Repository
{
    public class ChatsRepository : IChatsRepository
    {
        private AuthContext _context;
        public ChatsRepository(AuthContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Chat Get(string id)
        {
            try
            {
                return _context.Chats.Include("Users").SingleOrDefault(x => x.Id.ToString().Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Chat Remove(Chat instance)
        {
            try
            {
                var toDelete = _context.Chats.Include("Users").
                       SingleOrDefault(x => x.Id.Equals(instance.Id));
                var deleted = _context.Chats.Remove(toDelete);

                _context.SaveChanges();
                return deleted;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Chat Update(Chat instance)
        {
            throw new NotImplementedException();
        }

        public Chat UpdateTitle(Chat instance)
        {
            try
            {
                var chat = _context.Chats.Single(x => x.Id == instance.Id);
                chat.Title = instance.Title;
                _context.SaveChanges();

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
                var chat = _context.Chats.Single(x => x.Id == instance.Id);
                chat.Img = instance.Img;
                _context.SaveChanges();

                return chat;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetAllUsersCountForChat(Guid id)
        {
            try
            {
                var chat = _context.Chats.Include("Users").Single(x => x.Id == id);
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
                var chat = _context.Chats.Include("Users").Single(x => x.Id.ToString() == id);

                return chat?.Users.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Chat Add(Chat instance)
        {
            try
            {
                var result = _context.Chats.Add(instance);
                _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Chat> GetAll()
        {
            return _context.Chats.ToList();
        }

        public bool Quit(string chatId, string username)
        {
            try
            {
                var user = _context.Users.Single(x => x.UserName == username);
                var chat = _context.Chats.Include("Users").Single(x => x.Id == new Guid(chatId));
                chat.Users.Remove(user);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<Chat> GetAllChatsByUsername(string userName)
        {
            var chats = _context.Chats.Include("Users").Where(x =>
            x.Users.Count(y => y.UserName.Equals(userName)) > 0).ToList();
            return chats;
        }


    }
}
