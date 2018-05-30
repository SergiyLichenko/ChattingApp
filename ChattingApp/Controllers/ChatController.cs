using System;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/chat")]
    public class ChatController : ApiController
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var chats = await _chatRepository.GetAllAsync();
            return Ok(chats);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync([FromUri] int id)
        {
            if (id < 0) return BadRequest("Id cannot be negative");

            var chat = await _chatRepository.GetByIdAsync(id);
            return Ok(chat);
        }
    }
}