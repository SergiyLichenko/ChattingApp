using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChattingApp.Hubs
{
    [HubName("messageHub")]
    [RoutePrefix("api/message")]
    public class MessageHub : Hub
    {
        private static readonly IDictionary<string, HashSet<string>> ClientInfo;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        static MessageHub()
        {
            ClientInfo = new Dictionary<string, HashSet<string>>();
        }

        public MessageHub(
            IMessageRepository messageRepository,
            IChatRepository chatRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public override async Task OnConnected()
        {
            var currentUserId = Context.User.Identity.GetUserId();
            if (!ClientInfo.ContainsKey(currentUserId))
                ClientInfo[currentUserId] = new HashSet<string>();

            ClientInfo[currentUserId].Add(Context.ConnectionId);
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var currentUserId = Context.User.Identity.GetUserId();
            ClientInfo[currentUserId].Remove(Context.ConnectionId);
            await base.OnDisconnected(stopCalled);
        }

        public override async Task OnReconnected()
        {
            ClientInfo[Context.User.Identity.GetUserId()].Add(Context.ConnectionId);
            await base.OnReconnected();
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
            await NotifyClients(message.Chat.Id, client => client.onMessageUpdateAsync(message));
        }

        public async Task OnMessageDeleteAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _messageRepository.DeleteAsync(message);
            await NotifyClients(message.Chat.Id, client => client.onMessageDeleteAsync(message));
        }

        private async Task NotifyClients(int chatId, Action<dynamic> callback)
        {
            var currentChat = await _chatRepository.GetByIdAsync(chatId);

            foreach (var user in currentChat.Users)
            {
                if (!ClientInfo.ContainsKey(user.Id)) continue;
                NotifyClient(user.Id, callback);
            }
        }

        private void NotifyClient(string clientId, Action<dynamic> callback)
        {
            var connectionIds = ClientInfo[clientId];
            foreach (var connectionId in connectionIds)
            {
                var client = Clients.Client(connectionId);
                callback(client);
            }
        }
    }
}