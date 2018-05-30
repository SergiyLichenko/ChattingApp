using System;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Helpers.Translate.Interfaces;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly IMessageTranslator _messageTranslator;

        public MessageController(IMessageTranslator messageTranslator)
        {
            _messageTranslator = messageTranslator ?? throw new ArgumentNullException(nameof(messageTranslator));
        }

        [HttpGet]
        [Route("translate/{id}")]
        public async Task<IHttpActionResult> GetTranslate([FromUri] int id)
        {
            if (id < 0) return BadRequest("Id cannot be negative");

            var result = await _messageTranslator.TranslateAsync(id);
            return Ok(result);
        }
    }
}