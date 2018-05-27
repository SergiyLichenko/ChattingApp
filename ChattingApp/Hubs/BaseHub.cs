using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ChattingApp.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace ChattingApp.Hubs
{
    public class BaseHub : Hub
    {
        protected readonly IChatRepository ChatRepository;
        private static readonly IDictionary<string, HashSet<string>> ClientInfo;

        static BaseHub()
        {
            ClientInfo = new ConcurrentDictionary<string, HashSet<string>>();
        }

        protected BaseHub(IChatRepository chatRepository)
        {
            ChatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
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

        protected async Task NotifyClients(int chatId, Action<dynamic> callback)
        {
            var currentChat = await ChatRepository.GetByIdAsync(chatId);

            foreach (var user in currentChat.Users)
                if (user != null)
                    NotifyClient(user.Id, callback);
        }

        protected void NotifyClient(string clientId, Action<dynamic> callback)
        {
            if (!ClientInfo.ContainsKey(clientId)) return;

            var connectionIds = ClientInfo[clientId];
            foreach (var connectionId in connectionIds)
            {
                var client = Clients.Client(connectionId);
                callback(client);
            }
        }
    }
}