using System;
using System.Collections.Generic;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IChatsRepository : IRepository<Chat>
    {
        List<Chat> GetAllChatsByUsername(string userName);
        Chat Add(Chat instance);
        List<Chat> GetAll();
        bool Quit(string chatId, string username);
        Chat UpdateTitle(Chat chat);
        Chat UpdateImage(Chat chat);
        int GetAllUsersCountForChat(Guid id);
        List<ApplicationUser> GetAllUsersForChat(string id);
    }
}