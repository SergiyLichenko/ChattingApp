using System;
using System.Collections.Generic;
using System.Configuration;
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

            return await _authContext.Chats
                .Include(x => x.Users.Select(y => y.Language))
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            var authorId = Convert.ToInt32(HttpContext.Current.User.Identity.GetUserId());
            var author = await _userRepository.GetByIdAsync(authorId);

            chat.AuthorId = authorId;
            chat.Users = new List<ApplicationUser>() { author };
            chat.CreateDate = DateTime.Now;

            if (chat.Img == null) chat.Img = ConfigurationManager.AppSettings["DefaultImageUrl"];
            else if (!chat.Img.IsUrl())
                chat.Img = await ImageUploader.UploadAsync(chat.Img);

            _authContext.Chats.Add(chat);
            await _authContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            var existingChat = await GetByIdAsync(chat.Id);
            if (existingChat == null) throw new InvalidOperationException();

            var deletedUsers = existingChat.Users
                .Except(chat.Users, user => user.Id).ToList();
            var addedUsers = chat.Users.Except(existingChat.Users, x => x.Id).ToList();

            _authContext.Entry(existingChat).CurrentValues.SetValues(chat);

            if (chat.Img != null && !chat.Img.IsUrl())
                existingChat.Img = await ImageUploader.UploadAsync(chat.Img);

            foreach (var user in deletedUsers)
                user.Chats.Remove(existingChat);

            foreach (var user in addedUsers)
            {
                var existingUser = await _userRepository.GetByIdAsync(user.Id);
                existingUser.Chats.Add(existingChat);
            }

            await _authContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 0) throw new ArgumentNullException(nameof(id));

            var existingChat = await GetByIdAsync(id);
            if (existingChat == null) throw new InvalidOperationException();

            _authContext.Chats.Remove(existingChat);
            await _authContext.SaveChangesAsync();
        }
    }
}