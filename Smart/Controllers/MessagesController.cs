using System;
using System.Text;

namespace Smart.Controllers
{
    public class MessagesController : ApiController
    {
        private IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }


        /// <summary>
        /// Get all messages for chat
        /// </summary>
        /// <param name="id">chat ID</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var response = _messageService.GetAllMessagesFromChat(id);
                if (response != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        private string GetTextWithPlainTextInDivs(string message)
        {
            StringBuilder result = new StringBuilder();
            while (message.Contains("[CODE]") && message.Contains("[!CODE]"))
            {
                int start = 0;
                int end = message.IndexOf("[CODE]", StringComparison.InvariantCulture);
                if (end == start)
                {
                    end = message.IndexOf("[!CODE]", StringComparison.InvariantCulture) + 7;

                    //appending the part to delete to the result
                    string firstCode = message.Substring(start, end - start);
                    result.Append(firstCode);

                    //deleting appended part
                    message = message.Remove(start, end - start);
                }
                else
                {
                    result.Append("<div>" + message.Substring(start, end - start) + "</div>");
                    message = message.Remove(start, end - start);
                }
            }
            if (message.Length != 0)
                result.Append("<div>" + message + "</div>");
            return result.ToString();
        }

        [HttpPost]
        public HttpResponseMessage Post(string id)
        {
            if (ModelState.IsValid)
            {
                bool message = _messageService.MarkAsFavourite(id);
                if (message)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

       
    }
}
