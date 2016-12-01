using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smart.Models.Entities
{
   
   public class User
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<bool> IsPrivate { get; set; }
        public bool IsConfirmedByEmail { get; set; }
       
        public Role Role { get; set; }
        public ICollection<Chat> Chats { get; set; }

        public User()
        {
            Chats = new List<Chat>();
        }
    }

}