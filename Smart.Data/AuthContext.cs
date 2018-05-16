using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("name=MessengerConnectionString")
        {
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<FavouriteMessage> FavouriteMessages { get; set; }
    }

    public class MyInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Moderator"));
            context.Roles.Add(new IdentityRole("Admin"));

            if (context.Clients.Any()) return;
            context.Clients.AddRange(BuildClientsList());
            context.SaveChanges();
            base.Seed(context);
        }

        private static List<Client> BuildClientsList()
        {

            List<Client> ClientsList = new List<Client>
            {
                new Client
                { Id = "SMARTMessenger",
                    Secret= EncryptPassword.GetEncryptedPassword("WebMessengerSecret@Smart2016"),
                    Name="AngularJS front-end Application",
                    ApplicationType =  ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                { Id = "consoleApp",
                    Secret=EncryptPassword.GetEncryptedPassword("DesktopMessengerSecret@Smart2016"),
                    Name="Console Application",
                    ApplicationType =ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}