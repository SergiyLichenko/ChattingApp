using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using ChattingApp.Service;
using ChattingApp.Service.Models;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        private readonly IChatService _chatService;
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatService chatService, IChatRepository chatRepository)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var chats = await _chatRepository.GetAllAsync();
            return Ok(chats);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(Chat chat)
        {
            if (chat == null) return BadRequest("Data is null");

            await _chatRepository.AddAsync(chat);
            return Ok();
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            List<ChatViewModel> chats = _chatService.GetAllChats(id);
            return Request.CreateResponse(HttpStatusCode.OK, chats);
        }





        [HttpPost]
        [Route("quit")]
        public HttpResponseMessage QuitChat(QuitChatRequest request)
        {
            if (String.IsNullOrEmpty(request.ChatId) || string.IsNullOrEmpty(request.Username))
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
            bool result = _chatService.Quit(request.ChatId, request.Username);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }

        [HttpPost]
        [Route("edit")]
        public HttpResponseMessage EditChat(ChatViewModel chat)
        {
            if (string.IsNullOrEmpty(chat.Id.ToString()))
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            var updated = _chatService.Update(chat);
            if (updated != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(ChatViewModel chat)
        {
            if (chat.Id.ToString() != string.Empty && chat.AuthorName != null)
            {
                var removed = _chatService.Remove(chat);
                if (chat != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
