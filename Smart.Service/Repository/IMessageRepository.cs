using System.Collections.Generic;

namespace Smart.Service.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Add(Message instance);
        List<Message> GetAllMessagesFromChat(string id);
        Message GetMessageById(string id);
        bool MarkAsFavourite(string id);

    }
}