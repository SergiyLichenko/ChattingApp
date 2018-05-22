using System;
using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [AllowAnonymous]
        [Route("signup")]
        public async Task<IHttpActionResult> Register(UserDomain userModel)
        {
            if (userModel == null) return BadRequest("Author info cannot be null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userRepository.AddAsync(userModel);
            return Ok();
        }
    }
}