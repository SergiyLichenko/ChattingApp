﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ChattingApp.Service;
using ChattingApp.Service.Models;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/Chats")]
    public class ChatsController : ApiController
    {
        private IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {

            //Get all chats by username
            List<ChatViewModel> chats = _chatService.GetAllChats(id);
            return Request.CreateResponse(HttpStatusCode.OK, chats);
        }

        [HttpGet]
        [Route("All")]
        public HttpResponseMessage GetAll()
        {
            //Get all chats by username
            List<ChatViewModel> chats = _chatService.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, chats);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = new TimeSpan(1, 0, 0, 0)
            };
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post(ChatViewModel newChat)
        {
            if (ModelState.IsValid)
            {
                ChatViewModel chat = _chatService.Add(newChat);
                if (chat != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, chat);
                }

            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
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
