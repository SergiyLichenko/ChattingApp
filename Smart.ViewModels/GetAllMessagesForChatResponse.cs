using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.ViewModels
{
    public class GetAllMessagesForChatResponse
    {
        public List<MessageViewModel> Messages { get; set; }
        public Dictionary<string, string> UserImages { get; set; }
        public int CountAll { get; set; }
        public int UsersCount { get; set; }
    }
}
