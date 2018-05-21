using System.Data.Entity;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository.Helpers
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(IdentityDbContext<ApplicationUser> context) : base(context)
        {
        }

        public override async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await Users.Include(x => x.Chats)
                .Include(x => x.Roles)
                .Include(x => x.Claims)
                .Include(x => x.Logins)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}