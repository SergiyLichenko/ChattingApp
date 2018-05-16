using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            Chats = new List<Chat>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("IsPrivate", this.IsPrivate.ToString()));
            userIdentity.AddClaim(new Claim("Chats", this.Chats.ToString()));
            return userIdentity;
        }

        public ICollection<Chat> Chats { get; set; }
        public bool IsPrivate { get; set; }
        public string Img { get; set; }
        public string Id { get; }
        public string UserName { get; set; }
    }
}
