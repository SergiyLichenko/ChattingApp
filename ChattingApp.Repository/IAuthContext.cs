using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository
{
    public interface IAuthContext
    {
        IDbSet<Message> Messages { get; set; }
        IDbSet<Chat> Chats { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}