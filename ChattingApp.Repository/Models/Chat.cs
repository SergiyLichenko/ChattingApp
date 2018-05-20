using System;
using System.Collections.Generic;

namespace ChattingApp.Repository.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string Img { get; set; }
        public string AuthorId { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}