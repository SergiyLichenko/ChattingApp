using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Smart.Data;
using Smart.Entities;
using Smart.Models;
using Smart.Models.Contexts;
using Smart.Models.Entities;

namespace Smart.Data
{
    public class AuthContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public AuthContext()
            : base("name=MessengerConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        //   public DbSet<ReferrerMessage> ReferrerMessages { get; set; }
        public DbSet<FavouriteMessage> FavouriteMessages { get; set; }
    }

    public class MyInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Moderator"));
            context.Roles.Add(new IdentityRole("Admin"));
            if (context.Clients.Count() > 0)
            {
                return;
            }
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
                    ApplicationType =  Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                { Id = "consoleApp",
                    Secret=EncryptPassword.GetEncryptedPassword("DesktopMessengerSecret@Smart2016"),
                    Name="Console Application",
                    ApplicationType =Models.ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }

}