using System.Data.Entity;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}