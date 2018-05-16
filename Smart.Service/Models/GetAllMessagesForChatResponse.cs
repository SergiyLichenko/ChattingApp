﻿using System.Collections.Generic;

namespace ChattingApp.Service.Models
{
    public class GetAllMessagesForChatResponse
    {
        public List<MessageViewModel> Messages { get; set; }
        public Dictionary<string, string> UserImages { get; set; }
        public int CountAll { get; set; }
        public int UsersCount { get; set; }
    }
}
