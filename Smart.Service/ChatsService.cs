using System;
using System.Collections.Generic;
using System.Linq;
using Smart.Service.Repository;

namespace Smart.Service
{
    public class ChatsService : IChatsService
    {
        private IChatsRepository _chatsRepository;
        private readonly IUserRepository _userRepository;
        private IMappingService _mappingService;
        private IUserService _userService;
        public ChatsService(IChatsRepository chatsRepository,
            IUserRepository userRepository,
            IMappingService mappingService,
            IUserService userService)
        {
            _chatsRepository = chatsRepository;
            _userRepository = userRepository;
            _mappingService = mappingService;
            _userService = userService;
        }

        public ChatViewModel Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;
            Chat chat = _chatsRepository.Get(id);
            ChatViewModel chatViewModel = _mappingService.Map<Chat, ChatViewModel>(chat);
            return chatViewModel;
        }

        public ChatViewModel Remove(ChatViewModel instance)
        {
            if (instance == null || instance.Id.ToString().Equals(Guid.Empty.ToString()))
                return null;

            Chat chat = _mappingService.Map<ChatViewModel, Chat>(instance);

            Chat result = _chatsRepository.Remove(chat);
            return _mappingService.Map<Chat, ChatViewModel>(result);
        }

        public ChatViewModel Update(ChatViewModel instance)
        {
            if (instance == null || instance.Id.ToString().Equals(Guid.Empty.ToString()))
                return null;
            Chat chat = _mappingService.Map<ChatViewModel, Chat>(instance);
            Chat result = _chatsRepository.UpdateTitle(chat);

            if (string.IsNullOrEmpty(chat.Img))
            {
                chat.Img = _userService.GetDefaultImage();
                chat.Img = chat.Img.Insert(0, "data:image/png;base64,");
            }
            result = _chatsRepository.UpdateImage(chat);

            return _mappingService.Map<Chat, ChatViewModel>(result);
        }

        public ChatViewModel Add(ChatViewModel instance)
        {
            if (instance == null)
                return null;
            Chat chat = _mappingService.Map<ChatViewModel, Chat>(instance);
            chat.Id = Guid.NewGuid();
            chat.CreateDate = DateTime.Now;

            bool result = false;
            Chat newChat = _chatsRepository.Add(chat);
            if (newChat != null)
            {
                _userRepository.AddUserToChat(instance.AuthorName, newChat.Id.ToString());
                return _mappingService.Map<Chat, ChatViewModel>(newChat);
            }
            return null;
        }

        public List<ChatViewModel> GetAll()
        {
            var chats = _chatsRepository.GetAll();
            List<ChatViewModel> chatViewModel = _mappingService.Map<List<Chat>, List<ChatViewModel>>(chats);
            return chatViewModel;
        }

       

        public bool Quit(string chatId, string username)
        {
            try
            {
                return _chatsRepository.Quit(chatId, username);
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
            List<Chat> chats = _chatsRepository.GetAllChatsByUsername(userName);
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
