using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            IUserRepository userRepository,
            IChatRepository chatRepository)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public async Task AddAsync(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            message.CreateDate = DateTime.Now;
            message.Chat = await _chatRepository.GetByIdAsync(message.Chat.Id);
            message.Author = await _userRepository.GetByIdAsync(message.Author.Id);
            _authContext.Messages.Add(message);

            await _authContext.SaveChangesAsync();
        }










        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Message GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Message Remove(Message message)
        {
            try
            {
                // lock (_lock)
                {
                    var toDelete = _authContext.Messages.Include("Chat").
                            SingleOrDefault(x => x.Id.Equals(message.Id));
                    if (toDelete == null)
                    {
                        message.Chat = _authContext.Chats.Single(x => x.Id == message.Chat.Id);
                        return message;
                    }
                    //var deleted = _authContext.Messages.RemoveRange(new [] {toDelete });

                    _authContext.SaveChangesAsync();
                    //    return deleted.Single();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public Message UpdateAsync(Message instance)
        {
            try
            {
                // lock (_lock)
                {
                    var message = _authContext.Messages.Include("Chat").
                        Single(x => x.Id.ToString().Equals(instance.Id.ToString()));
                    message.Chat = _authContext.Chats.Include("Users").Single(x => x.Id == message.Chat.Id);
                    //  message.Author = _authContext.Users.Single(x => x.UserName == instance.Author.UserName);
                    message.Text = instance.Text;
                    _authContext.SaveChangesAsync();
                    return message;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Message Add(Message instance)
        {
            try
            {
                //lock (_lock)
                {
                    // var user = _authContext.Users.SingleOrDefault(x => x.UserName.Equals(instance.Author.UserName));
                    var chat = _authContext.Chats.Include("Users").SingleOrDefault(x => x.Id.ToString().Equals(instance.Chat.Id.ToString()));
                    //if (user == null || chat == null || !chat.Users.Contains(user))
                    //    throw new Exception("Error");

                    //     instance.Author = user;
                    instance.Chat = chat;
                    // instance.UserId = new Guid(user.Id);
                    //   _authContext.Messages.AddRange(new [] {instance });
                    //     int count = _authContext.SaveChangesAsync();
                    return instance;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Message GetMessageById(string id)
        {
            try
            {
                // lock (_lock)
                {
                    Message message = _authContext.Messages.Single(x => x.Id.ToString().Equals(id));
                    return message;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool MarkAsFavourite(string id)
        {
            try
            {
                _authContext.Messages.Find(id).IsFavourite = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<Message> GetAllMessagesFromChat(string id)
        {
            try
            {
                // lock (_lock)
                {
                    //List<Message> messages = _authContext.Messages.Include("Author").
                    //    Include("Chat").Where(x => x.ChatId.ToString().Equals(id)).ToList();
                    //return messages;
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
