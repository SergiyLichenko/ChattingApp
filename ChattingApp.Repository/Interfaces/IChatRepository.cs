using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IChatRepository 
    {
        Task<List<Chat>> GetAllAsync();
        Task<Chat> GetByIdAsync(int id);
        Task AddAsync(Chat instance);
        Task UpdateAsync(Chat chat);

        List<Chat> GetAllChatsByUsername(string userName);
        
       
        bool Quit(string chatId, string username);
        Chat UpdateTitle(Chat chat);
        Chat UpdateImage(Chat chat);
        int GetAllUsersCountForChat(int id);
        List<ApplicationUser> GetAllUsersForChat(string id);
    }
}