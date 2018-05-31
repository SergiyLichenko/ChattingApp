using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IAuthContext _authContext;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public MessageRepository(IAuthContext authContext,
            IChatRepository chatRepository,
            IUserRepository userRepository)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentNullException(nameof(id));

            return await _authContext.Messages.Include(x => x.Chat)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            message.Author = await _userRepository.GetByIdAsync(message.Author.Id);
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
            existingMessage.Author = await _userRepository.GetByIdAsync(message.Author.Id);
            existingMessage.Text = message.Text;
            existingMessage.IsFavorite = message.IsFavorite;

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