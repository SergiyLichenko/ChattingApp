using System.Threading.Tasks;

namespace Smart.Service
{
    public interface IUserService : IService<UserViewModel>
    {
        UserViewModel GetByUserNameAndPassword(string username, string password);
        bool AddUserToChat(string username, string chatTitle);
        Task<ApplicationUser> FindAsync(UserLoginInfo userLoginInfo);
        IdentityResult Add(UserViewModel instance);
        UserViewModel GetUserByName(string name);
        bool AddImage(string userId, byte[] imageByteArray);
        UserViewModel Update(UserViewModel oldInstance, UserViewModel newInstance, string oldPassword);
        string GetDefaultImage();
    }
}