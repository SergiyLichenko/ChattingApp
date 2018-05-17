using System.Web.Http;
using ChattingApp.Service;
using ChattingApp.Service.Models;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService service)
        {
            _userService = service;
        }

        [AllowAnonymous]
        [Route("signUp")]
        public IHttpActionResult Register(UserViewModel userModel)
        {
            if (userModel == null) return BadRequest("User info cannot be null");
            if (!ModelState.IsValid) BadRequest(ModelState);

            _userService.Add(userModel);
            return Ok();
        }
    }
}