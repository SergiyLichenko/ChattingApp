namespace ChattingApp.Service.Models
{
    public class AddUserToChatHubRequest
    {
        public ChatViewModel Chat { get; set; }
        public UserViewModel User { get; set; }
    }
}
