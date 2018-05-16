using System;
using System.Collections.Generic;

namespace Smart.Service.Repository
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