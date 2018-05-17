using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ChattingApp.Repository;
using ChattingApp.Repository.Repository;
using ChattingApp.Service;
using ChattingApp.Service.Models;
using Microsoft.AspNet.SignalR;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace ChattingApp.Controllers
{

    public class MessageHub : Hub
    {
        private IMessageService _messageService;
        private IChatsService _chatsService;
        private IUserService _userService;
        private readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(MessageViewModel));
        public MessageHub()
        {
            this._messageService = new MessageService(new MessageRepository(new AuthContext()), new MappingService(), new ChatsRepository(new AuthContext()));
            _chatsService = new ChatsService(new ChatsRepository(new AuthContext()), new UserRepository(), new MappingService(), new UserService(new UserRepository(), new MappingService()));
            _userService = new UserService(new UserRepository(), new MappingService());
        }
        private static Dictionary<string, string> _users_ConnectionIds = new Dictionary<string, string>();
        public override Task OnConnected() => base.OnConnected();

        [Authorize]
        public void RegisterMe(string userName)
        {
            if (_users_ConnectionIds.ContainsValue(userName))
            {
                for (int i = 0; i < _users_ConnectionIds.Count; i++)
                {
                    if (_users_ConnectionIds[_users_ConnectionIds.Keys.ElementAt(i)] == userName)
                    {
                        _users_ConnectionIds.Remove(_users_ConnectionIds.Keys.ElementAt(i));
                    }
                }
            }

            _users_ConnectionIds.Add(Context.ConnectionId, userName);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            _users_ConnectionIds.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
       [Authorize]
        public void UserToChat(ChatViewModel chat, string userName)
        {
            try
            {
                if (_userService.AddUserToChat(userName, chat.Id.ToString()))
                {
                    var c = _chatsService.Get(chat.Id.ToString());
                    var user = _userService.GetUserByName(userName);

                    foreach (var item in c.Users)
                    {
                        var result = _users_ConnectionIds.SingleOrDefault(x => x.Value.Equals(item.userName)).Key;
                        if (result != null)
                        {
                            Clients.Client(result).OnAddUserToChat(user);
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        [Authorize]
        public void SendMessage(MessageViewModel newMessage)
        {
            if (newMessage == null)
                return;

            MessageViewModel createdMessage = _messageService.Add(newMessage);

            if (createdMessage != null)
            {
                createdMessage.user.img = null;
                createdMessage.chat.Img = null;
                createdMessage.chat.Users = createdMessage.chat.Users.Select(x =>
                {
                    x.img = null;
                    return x;
                }).ToList();

                foreach (var item in createdMessage.chat.Users)
                {
                    var result = _users_ConnectionIds.SingleOrDefault(x => x.Value.Equals(item.userName)).Key;
                    if (result != null)
                    {
                        Clients.Client(result).OnMessage(createdMessage);
                    }
                }
            }
        }
        public void UpdateMessage(MessageViewModel message)
        {
            if (message == null)
                return;
            MessageViewModel messageViewModel = _messageService.Update(message);

            if (messageViewModel != null)
            {
                messageViewModel.user.img = null;
                messageViewModel.chat.Img = null;
                messageViewModel.chat.Users = messageViewModel.chat.Users.Select(x =>
                {
                    x.img = null;
                    return x;
                }).ToList();
                foreach (var item in messageViewModel.chat.Users)
                {
                    var result = _users_ConnectionIds.SingleOrDefault(x => x.Value.Equals(item.userName)).Key;
                    if (result != null)
                    {
                        Clients.Client(result).OnUpdateMessage(messageViewModel);
                    }
                }
            }
        }





        [Authorize]
        public void DeleteMessage(MessageViewModel message)
        {
            if (message == null)
                return;
            if (!_users_ConnectionIds.ContainsKey(Context.ConnectionId))
                _users_ConnectionIds.Add(Context.ConnectionId, message.user.userName);
            if (!message.user.userName.Equals(_users_ConnectionIds[Context.ConnectionId])) return;

            MessageViewModel deletedMessage = _messageService.Remove(message);
            if (deletedMessage != null)
            {
                if (deletedMessage.chat != null)
                {
                    deletedMessage.chat.Img = null;
                    if (deletedMessage.chat.Users != null)
                    {
                        deletedMessage.chat.Users = deletedMessage.chat.Users.Select(x =>
                        {
                            x.img = null;
                            return x;
                        }).ToList();
                    }
                }

                foreach (var item in deletedMessage.chat.Users)
                {
                    var result = _users_ConnectionIds.SingleOrDefault(x => x.Value.Equals(item.userName)).Key;
                    if (result != null)
                    {
                        Clients.Client(result).OnDeleteMessage(deletedMessage);
                    }
                }
            }
        }
    }
}