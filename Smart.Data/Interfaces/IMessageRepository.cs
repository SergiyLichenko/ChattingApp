using System.Collections.Generic;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Add(Message instance);
        List<Message> GetAllMessagesFromChat(string id);
        Message GetMessageById(string id);
        bool MarkAsFavourite(string id);

    }
}