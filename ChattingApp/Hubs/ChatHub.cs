using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChattingApp.Hubs
{
    [HubName("chatHub")]
    [RoutePrefix("api/chat-hub")]
    public class ChatHub : BaseHub
    {
        public ChatHub(IChatRepository chatRepository) : base(chatRepository)
        {
        }

        public async Task OnChatCreateAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            await ChatRepository.AddAsync(chat);
            await NotifyClients(chat.Id, client => client.onChatCreateAsync(chat));
        }

        public async Task OnChatUpdateAsync(Chat chat)
        {
            if (chat == null) throw new ArgumentNullException(nameof(chat));

            var clientIds = chat.Users.Select(x => x.Id).ToList();
            var existingChat = await ChatRepository.GetByIdAsync(chat.Id);
            clientIds.AddRange(existingChat.Users.Select(x => x.Id).ToList());

            await ChatRepository.UpdateAsync(chat);

            existingChat = await ChatRepository.GetByIdAsync(chat.Id);
            var existingUsers = existingChat.Users.Select(x => x.Id).ToList();

            foreach (var clientId in clientIds)
            {
                if (existingUsers.Contains(clientId))
                    NotifyClient(clientId.ToString(), client => client.onChatUpdateAsync(existingChat));
                else
                    NotifyClient(clientId.ToString(), client => client.onChatDeleteAsync(existingChat.Id));
            }
        }

        public async Task OnChatDeleteAsync(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            var existingChat = await ChatRepository.GetByIdAsync(id);
            var clientIds = existingChat.Users.Select(x => x.Id).ToList();

            await ChatRepository.DeleteAsync(id);
            foreach (var clientId in clientIds)
                NotifyClient(clientId.ToString(), client => client.onChatDeleteAsync(id));
        }
    }
}