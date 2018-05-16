using System;
using System.Collections.Generic;

namespace Smart.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private AuthContext _context;
        private static readonly object _lock = new object();
        public MessageRepository(IDataContext context)
        {
            _context = (AuthContext)context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Message Get(string id)
        {
            throw new NotImplementedException();
        }

        public Message Remove(Message message)
        {
            try
            {
                lock (_lock)
                {
                    var toDelete = _context.Messages.Include("Chat").
                            SingleOrDefault(x => x.Id.Equals(message.Id));
                    if (toDelete == null)
                    {
                        message.Chat = _context.Chats.Single(x => x.Id == message.Chat.Id);
                        return message;
                    }
                    var deleted = _context.Messages.Remove(toDelete);

                    _context.SaveChanges();
                    return deleted;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Message Update(Message instance)
        {
            try
            {
                lock (_lock)
                {
                    var message = _context.Messages.Include("Chat").
                        Single(x => x.Id.ToString().Equals(instance.Id.ToString()));
                    message.Chat = _context.Chats.Include("Users").Single(x => x.Id == message.Chat.Id);
                    message.User = _context.Users.Single(x => x.UserName == instance.User.UserName);
                    message.Text = instance.Text;
                    _context.SaveChanges();
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
                lock (_lock)
                {
                    var user = _context.Users.SingleOrDefault(x => x.UserName.Equals(instance.User.UserName));
                    var chat = _context.Chats.Include("Users").SingleOrDefault(x => x.Id.ToString().Equals(instance.Chat.Id.ToString()));
                    if (user == null || chat == null || !chat.Users.Contains(user))
                        throw new Exception("Error");

                    instance.User = user;
                    instance.Chat = chat;
                    instance.UserId = new Guid(user.Id);
                    _context.Messages.Add(instance);
                    int count = _context.SaveChanges();
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
                lock (_lock)
                {
                    Message message = _context.Messages.Single(x => x.Id.ToString().Equals(id));
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
                _context.Messages.Find(id).IsFavourite = true;
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
                lock (_lock)
                {
                    List<Message> messages = _context.Messages.Include("User").
                        Include("Chat").Where(x => x.ChatId.ToString().Equals(id)).ToList();
                    return messages;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
