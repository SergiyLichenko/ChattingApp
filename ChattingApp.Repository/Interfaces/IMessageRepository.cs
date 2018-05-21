using System.Collections.Generic;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task AddAsync(Message message);



        Message Add(Message instance);
        List<Message> GetAllMessagesFromChat(string id);
        Message GetMessageById(string id);
        bool MarkAsFavourite(string id);

    }
}