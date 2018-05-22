using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task DeleteAsync(Message message);
    }
}