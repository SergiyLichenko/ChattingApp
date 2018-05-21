using System.Data.Entity;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository
{
    public class AuthContext : IdentityDbContext<ApplicationUser>, IAuthContext
    {
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>().HasKey(x => x.Id);
            modelBuilder.Entity<Chat>().HasMany(x => x.Users).WithMany(x => x.Chats);
            modelBuilder.Entity<Chat>().Property(x => x.CreateDate).IsRequired();
            modelBuilder.Entity<Chat>().Property(x => x.AuthorId).IsRequired();
            modelBuilder.Entity<Chat>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<Chat>().HasMany(x => x.Messages).WithRequired(x => x.Chat);

            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Chats).WithMany(x => x.Users);

            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Message>().Property(x => x.CreateDate).IsRequired();
            modelBuilder.Entity<Message>().HasRequired(x => x.Author);
            modelBuilder.Entity<Message>().HasRequired(x => x.Chat).WithMany(x => x.Messages);
        }
    }
}