using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
  /*  public class ApplicationUser: IdentityUser
    {
      

        public ApplicationUser()
        {
            Chats = new List<Chat>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            userIdentity.AddClaim(new Claim("IsPrivate", this.IsPrivate.ToString()));
            userIdentity.AddClaim(new Claim("Chats", this.Chats.ToString()));
            return userIdentity;
        }

        // Your Extended Properties
        
        public ICollection<Chat> Chats { get; set; }
        public bool IsPrivate { get; set; }

        public string Img { get; set; }
    }*/
}
