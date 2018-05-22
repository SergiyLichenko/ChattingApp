using System;
using System.Collections.Generic;
using ChattingApp.Repository.Models;

namespace ChattingApp.Service.Models
{
    public class ChatViewModel
    {
        public System.Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }

        public ICollection<UserDomain> Users { get; set; }
        public string Img{ get; set; }
    }
}
