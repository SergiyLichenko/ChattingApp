﻿using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ChattingApp.Service;
using ChattingApp.Service.Models;

namespace ChattingApp.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userService;
        private IChatService _chatService;
        public UserController(IUserService userService, IChatService chatService)
        {
            _userService = userService;
            _chatService = chatService;
        }

        [HttpPost]
        [ActionName("FileUpload")]
        public IHttpActionResult PostFileUpload()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string userId = "0";
                // Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                if (httpPostedFile != null)
                {
                    int length = httpPostedFile.ContentLength;
                    var imageByteArray = new byte[length];
                    httpPostedFile.InputStream.Read(imageByteArray, 0, length);

                    _userService.AddImage(userId, imageByteArray);

                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                    // Save the uploaded file to "UploadedFiles" folder  

                    httpPostedFile.SaveAs(fileSavePath);
                    return Ok("Image Uploaded");
                }
            }
            return Ok("Image is not Uploaded");
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            UserViewModel user = _userService.GetUserByName(id);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [Route("api/user/ToChat")]
        public HttpResponseMessage Post(AddUserToChatViewModel obj)
        {

            if (ModelState.IsValid)
            {
                _userService.AddUserToChat(obj.UserName, obj.ChatId);
                var chat = _chatService.Get(obj.ChatId);
                var user = _userService.GetUserByName(obj.UserName);
                chat.Img = null;
                user.Img = null;
                var response = new AddUserToChatControllerResponse()
                {
                    User = user,
                    Chat = chat
                };
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("api/user/edit")]
        public HttpResponseMessage EditUser(UpdateUserRequest request)
        {
            var updatedUser = _userService.Update(request.OldUser, request.NewUser, request.OldPassword);
            if (updatedUser != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, updatedUser);

            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}