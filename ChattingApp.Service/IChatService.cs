using System.Collections.Generic;
using ChattingApp.Service.Models;

namespace ChattingApp.Service
{
    public interface IChatService : IService<ChatViewModel>
    {
        List<ChatViewModel> GetAllChats(string userName);
        ChatViewModel Add(ChatViewModel instance);

        List<ChatViewModel> GetAll();
        bool Quit(string chatId, string username);
    }
}