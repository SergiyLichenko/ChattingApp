using System.Threading.Tasks;
using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;

namespace ChattingApp.Service
{
    public interface IUserService : IService<UserViewModel>
    {
        UserViewModel GetByUserNameAndPassword(string username, string password);
        bool AddUserToChat(string username, string chatTitle);
        //Task<ApplicationUser> FindAsync(UserLoginInfo userLoginInfo);
        void Add(UserViewModel instance);
        UserViewModel GetUserByName(string name);
        bool AddImage(string userId, byte[] imageByteArray);
        UserViewModel Update(UserViewModel oldInstance, UserViewModel newInstance, string oldPassword);
        string GetDefaultImage();
    }
}