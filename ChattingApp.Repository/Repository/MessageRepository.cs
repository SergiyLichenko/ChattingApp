using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Repository.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IAuthContext _authContext;
        private readonly IChatRepository _chatRepository;

        public MessageRepository(IAuthContext authContext,
            IChatRepository chatRepository)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentNullException(nameof(id));

            return await _authContext.Messages.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            message.CreateDate = DateTime.Now;
            message.Chat = await _chatRepository.GetByIdAsync(message.Chat.Id);
            _authContext.Messages.Add(message);

            await _authContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var existingChat = await _chatRepository.GetByIdAsync(message.Chat.Id);
            var existingMessage = await GetByIdAsync(message.Id);
            existingMessage.CreateDate = DateTime.Now;
            existingMessage.Chat = existingChat;
            existingMessage.AuthorId = HttpContext.Current.User.Identity.GetUserId();
            existingMessage.Text = message.Text;

            await _authContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var existingMessage = await GetByIdAsync(message.Id);
            _authContext.Messages.Remove(existingMessage);
            await _authContext.SaveChangesAsync();
        }
    }
}