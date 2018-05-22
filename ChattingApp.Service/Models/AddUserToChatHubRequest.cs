using ChattingApp.Repository.Models;

namespace ChattingApp.Service.Models
{
    public class AddUserToChatHubRequest
    {
        public ChatViewModel Chat { get; set; }
        public UserDomain User { get; set; }
    }
}
