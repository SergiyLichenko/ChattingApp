using System.Threading.Tasks;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(int id);
        Task UpdateAsync(UserDomain user);
        Task AddAsync(UserDomain user);
        Task<ApplicationUser> FindAsync(string userName, string password);
    }
}