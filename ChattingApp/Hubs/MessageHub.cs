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
        private static readonly IDictionary<string, string> ClientInfo;
        private readonly IMessageRepository _messageRepository;

        static MessageHub()
        {
            ClientInfo = new Dictionary<string, string>();
        }

        public MessageHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public override Task OnConnected()
        {
            ClientInfo[Context.User.Identity.GetUserId()] = Context.ConnectionId;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ClientInfo.Remove(Context.User.Identity.GetUserId());
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            ClientInfo[Context.User.Identity.GetUserId()] = Context.ConnectionId;
            return base.OnReconnected();
        }

        public async Task OnMessageCreateAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _messageRepository.AddAsync(message);
            foreach (var user in message.Chat.Users)
            {
                var clientId = ClientInfo[user.Id];
                Clients.Client(clientId).onMessageCreateAsync(message);
            }
        }
    }
}