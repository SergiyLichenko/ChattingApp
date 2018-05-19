using System.Threading.Tasks;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        bool AddUserToChat(string username, string chatTitle);
        void Add(ApplicationUser instance);
        ApplicationUser GetUserByName(string name);
        bool AddImage(string userId, byte[] imageByteArray);
        ApplicationUser ChangeEmail(string username, string newEmail);
        ApplicationUser ChangeUsername(string oldUsername, string newUsername);
        Task<ApplicationUser> FindAsync(string userName, string password);
    }
}