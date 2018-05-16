using System.Collections.Generic;

namespace Smart.Service
{
    public interface IChatsService : IService<ChatViewModel>
    {
        List<ChatViewModel> GetAllChats(string userName);
        ChatViewModel Add(ChatViewModel instance);

        List<ChatViewModel> GetAll();
        bool Quit(string chatId, string username);
    }
}