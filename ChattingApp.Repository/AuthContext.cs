using System.Data.Entity;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository
{
    public class AuthContext : IdentityDbContext<ApplicationUser>, IAuthContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>().HasKey(x => x.Id);
           // modelBuilder.Entity<Chat>().HasRequired(x => x.Title);
            modelBuilder.Entity<Chat>().HasMany(x => x.Users).WithMany(x => x.Chats);

            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Chats).WithMany(x => x.Users);
        }
    }
}