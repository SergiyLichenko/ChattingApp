using ChattingApp.Repository.Models;

namespace ChattingApp.Service.Models
{
public    class AddUserToChatControllerResponse
    {
        public ChatViewModel Chat { get; set; }
        public UserDomain User { get; set; }
    }
}
