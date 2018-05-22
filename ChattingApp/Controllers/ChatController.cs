using System;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;

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

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(Chat chat)
        {
            if (chat == null) return BadRequest("Data is null");

            await _chatRepository.AddAsync(chat);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(Chat chat)
        {
            if (chat == null) return BadRequest("Data is null");

            await _chatRepository.UpdateAsync(chat);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync([FromUri] int id)
        {
            if (id < 0) return BadRequest("Id cannot be negative");

            var chat = await _chatRepository.GetByIdAsync(id);
            return Ok(chat);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync([FromBody] int id)
        {
            if (id < 0) return BadRequest("Id cannot be negative");

            await _chatRepository.DeleteAsync(id);
            return Ok();
        }
    }
}