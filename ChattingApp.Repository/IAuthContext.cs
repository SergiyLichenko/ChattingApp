using System.Data.Entity;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository
{
    public interface IAuthContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<Chat> Chats { get; set; }
        Task<int> SaveChangesAsync();
    }
}