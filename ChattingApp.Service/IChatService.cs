using System.Collections.Generic;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;

namespace ChattingApp.Service
{
    public interface IChatService : IService<ChatViewModel>
    {

        List<ChatViewModel> GetAllChats(string userName);

        
        bool Quit(string chatId, string username);
    }
}