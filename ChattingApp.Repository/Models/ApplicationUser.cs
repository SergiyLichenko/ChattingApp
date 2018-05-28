using System.Collections.Generic;

namespace ChattingApp.Repository.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public string Img { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}