using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Smart.Data;

namespace Smart.Models.Entities
{

    public class Chat
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public System.DateTime CreateDate { get; set; }

        [Required]
        public string AuthorName { get; set; }//Author ID
        public ICollection<ApplicationUser> Users { get; set; }

        public string Img { get; set; }

        public Chat()
        {
            Users = new List<ApplicationUser>();
        }
    }
}