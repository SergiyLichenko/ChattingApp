using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChattingApp.Repository.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Chat> Chats { get; set; }
        public string Img { get; set; }
    }
}