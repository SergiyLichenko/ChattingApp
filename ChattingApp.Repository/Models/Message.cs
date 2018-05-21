using System;

namespace ChattingApp.Repository.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsModified { get; set; }
        public string AuthorId { get; set; }
        public Chat Chat { get; set; }
    }
}