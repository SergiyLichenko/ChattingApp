using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<List<Chat>> GetAllAsync();
        Task AddAsync(Chat instance);

        List<Chat> GetAllChatsByUsername(string userName);
        
       
        bool Quit(string chatId, string username);
        Chat UpdateTitle(Chat chat);
        Chat UpdateImage(Chat chat);
        int GetAllUsersCountForChat(int id);
        List<ApplicationUser> GetAllUsersForChat(string id);
    }
}