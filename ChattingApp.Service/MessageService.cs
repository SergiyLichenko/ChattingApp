using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChattingApp.Repository.Helpers;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;

namespace ChattingApp.Service
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        private IMappingService _mappingService;
        private IChatRepository _chatRepository;
        private const int UserImageMessageSize = 125;
        public MessageService(IMessageRepository messageRepository, IMappingService mappingService, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _mappingService = mappingService;
            _chatRepository = chatRepository;
        }

        public MessageViewModel Get(string id)
        {
            //if (String.IsNullOrWhiteSpace(id))
                return null;
            //Message message = _messageRepository.GetByIdAsync(id);
            //MessageViewModel messageViewModel = _mappingService.
            //    Map<Message, MessageViewModel>(message);
            //return messageViewModel;
        }

        public MessageViewModel Remove(MessageViewModel messageViewModel)
        {
            if (messageViewModel == null)
                return null;
            Message message = _mappingService.Map<MessageViewModel, Message>(messageViewModel);

            Message result = _messageRepository.Remove(message);
           // result.Chat = _chatRepository.GetByIdAsync(result.ChatId.ToString());

            return _mappingService.Map<Message, MessageViewModel>(result);
        }

        public MessageViewModel Update(MessageViewModel instance)
        {
            if (instance == null || instance.id.ToString().Equals(Guid.Empty.ToString()))
                return null;
            Message message = _mappingService.Map<MessageViewModel, Message>(instance);
            message.IsModified = true;

            Message result = _messageRepository.UpdateAsync(message);
            return _mappingService.Map<Message, MessageViewModel>(result);
        }

        public MessageViewModel GetMessageById(string id)
        {
            if (String.IsNullOrEmpty(id))
                return null;
            try
            {
                Message result = _messageRepository.GetMessageById(id);
                return _mappingService.Map<Message, MessageViewModel>(result);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MessageViewModel Add(MessageViewModel instance)
        {
            if (instance == null)
                return null;
            StringBuilder sb = new StringBuilder(instance.text);
            sb.Replace("&", "&amp;");
            sb.Replace(">", "&gt;");
            sb.Replace("<", "&lt;");

            instance.text = sb.ToString();//AddCodeTags(sb.ToString());
            try
            {
                Message message = _mappingService.Map<MessageViewModel, Message>(instance);
                message.CreateDate = DateTime.Now;
                //message.Id = Guid.NewGuid();
                message.IsModified = false;
                Message result = _messageRepository.Add(message);
                return _mappingService.Map<Message, MessageViewModel>(result);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool MarkAsFavourite(string id)
        {
            return _messageRepository.MarkAsFavourite(id);
        }


        public GetAllMessagesForChatResponse GetAllMessagesFromChat(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            List<Message> messages = _messageRepository.GetAllMessagesFromChat(id);
            List<MessageViewModel> result = messages.Select(x => AutoMapper.Mapper.Map<Message, MessageViewModel>(x)).ToList();
            var response = BuildMessagesResponse(result.OrderBy(x => x.createDate).ToList());
            var allUsers = _chatRepository.GetAllUsersForChat(id);
            /*var images = allUsers.ToDictionary(x => x.Id, x =>
             {
                 var Img = ImageResizer.ProcessImage(x.Img, UserImageMessageSize);
                 return Img.Insert(0, "data:image/jpg;base64,");
             });
            foreach (var item in images)
            {
                if (!response.UserImages.ContainsKey(item.Key))
                {
                    response.UserImages.AddAsync(item.Key, item.Value);
                }
            }*/
            return response;
        }

        private GetAllMessagesForChatResponse BuildMessagesResponse(List<MessageViewModel> messages)
        {
            var result = new GetAllMessagesForChatResponse
            {
                Messages = new List<MessageViewModel>(),
                CountAll = messages.Count,
               // UsersCount = messages.Count > 0 ? _chatRepository.GetAllUsersCountForChat(messages[0].chat.Id) : 0,
                UserImages = new Dictionary<string, string>()
            };

            foreach (var item in messages)
            {
                var temp = item;
                temp.user.Img = ImageResizer.ProcessImage(temp.user.Img, UserImageMessageSize);
                temp.user.Img = temp.user.Img.Insert(0, "data:image/jpg;base64,");
                if (!result.UserImages.ContainsKey(temp.user.Id.ToString()))
                {
                    result.UserImages.Add(temp.user.Id.ToString(), temp.user.Img);
                }

                temp.user.Img = null;
                temp.chat.Img = null;
                result.Messages.Add(item);
            }

            return result;
        }

        private string AddCodeTags(string messageText)
        {
            //Regex regex = new Regex("\[CODE\](.+)\[!CODE\]");
            //var v = regex.Match(messageText);
            if (messageText.Contains("[CODE]") && messageText.Contains("[!CODE]"))
                /*foreach (Group code in v.Groups)
                {
                    if (code.ToString() != "")*/
                messageText = messageText.Replace("[CODE]",
                    "<pre class=prettyprint><xmp>").Replace("[!CODE]", "</xmp></pre>");
            //}

            return messageText;
        }
    }
}
