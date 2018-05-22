using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.AspNet.Identity;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [Route("current")]
        public async Task<IHttpActionResult> GetCurrentAsync()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = await _userRepository.GetByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(UserDomain user)
        {
            if (user == null) return BadRequest("Request cannot be null");

            await _userRepository.UpdateAsync(user);
            return Ok();
        }
    }
}