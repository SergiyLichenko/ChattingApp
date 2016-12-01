using System.Collections.Generic;
using Smart.Models.Entities;

namespace Smart.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Add(Message instance);
        List<Message> GetAllMessagesFromChat(string id);
        Message GetMessageById(string id);
        bool MarkAsFavourite(string id);

    }
}