using System.Threading.Tasks;

namespace Smart.Service.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetByUserNameAndPassword(string userName, string password);
        bool AddUserToChat(string username, string chatTitle);

        Task<ApplicationUser> FindAsync(UserLoginInfo userLogInfo);
        IdentityResult Add(ApplicationUser instance);
        ApplicationUser GetUserByName(string name);
        bool AddImage(string userId, byte[] imageByteArray);
        void ChangePassword(string username, string currentPassword, string newPassword);
        ApplicationUser ChangeEmail(string username, string newEmail);
        ApplicationUser ChangeUsername(string oldUsername, string newUsername);
        ApplicationUser ChangeImage(string oldUsername, string img);
    }
}