using System;

namespace ChattingApp.Repository.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime CreateDate { get; set; }
        public ApplicationUser Author { get; set; }
        public Chat Chat { get; set; }
    }
}