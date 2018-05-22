using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Service
{
    public class ChatService : IChatService
    {
        private IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private IMappingService _mappingService;
        public ChatService(IChatRepository chatRepository,
            IUserRepository userRepository,
            IMappingService mappingService)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _mappingService = mappingService;
        }


       








        public ChatViewModel Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;
            return null;
            ////Chat chat = _chatRepository.GetByIdAsync(id);
            //ChatViewModel chatViewModel = _mappingService.Map<Chat, ChatViewModel>(chat);
            //return chatViewModel;
        }

        public ChatViewModel Remove(ChatViewModel instance)
        {
            if (instance == null || instance.Id.ToString().Equals(Guid.Empty.ToString()))
                return null;

            Chat chat = _mappingService.Map<ChatViewModel, Chat>(instance);

          //  Chat result = _chatRepository.Remove(chat);
           // return _mappingService.Map<Chat, ChatViewModel>(result);
            return null;
        }

        public ChatViewModel Update(ChatViewModel instance)
        {
            if (instance == null || instance.Id.ToString().Equals(Guid.Empty.ToString()))
                return null;
            Chat chat = _mappingService.Map<ChatViewModel, Chat>(instance);
            Chat result = _chatRepository.UpdateTitle(chat);

            if (string.IsNullOrEmpty(chat.Img))
            {
               // chat.Img = _userService.GetDefaultImage();
                chat.Img = chat.Img.Insert(0, "data:image/png;base64,");
            }
            result = _chatRepository.UpdateImage(chat);

            return _mappingService.Map<Chat, ChatViewModel>(result);
        }

        

        

       

        public bool Quit(string chatId, string username)
        {
            try
            {
                return _chatRepository.Quit(chatId, username);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ChatViewModel> GetAllChats(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
                return null;
            List<Chat> chats = _chatRepository.GetAllChatsByUsername(userName);
            try
            {
                List<ChatViewModel> result = chats.Select(x => AutoMapper.Mapper.Map<Chat, ChatViewModel>(x)).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}
