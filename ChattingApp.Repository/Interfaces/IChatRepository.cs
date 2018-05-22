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
        Task DeleteAsync(int id);
    }
}