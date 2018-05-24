using System;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChattingApp.Hubs
{
    [HubName("messageHub")]
    [RoutePrefix("api/message-hub")]
    public class MessageHub : BaseHub
    {
        private readonly IMessageRepository _messageRepository;

        public MessageHub(
            IMessageRepository messageRepository,
            IChatRepository chatRepository):base(chatRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public async Task OnMessageCreateAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _messageRepository.AddAsync(message);
            message = await _messageRepository.GetByIdAsync(message.Id);
            await NotifyClients(message.Chat.Id, client => client.onMessageCreateAsync(message));
        }

        public async Task OnMessageUpdateAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _messageRepository.UpdateAsync(message);
            message = await _messageRepository.GetByIdAsync(message.Id);
            await NotifyClients(message.Chat.Id, client => client.onMessageUpdateAsync(message));
        }

        public async Task OnMessageDeleteAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _messageRepository.DeleteAsync(message);
            await NotifyClients(message.Chat.Id, client => client.onMessageDeleteAsync(message));
        }
    }
}