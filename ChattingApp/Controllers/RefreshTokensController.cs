﻿using System.Threading.Tasks;
using System.Web.Http;
using ChattingApp.Repository.Repository;

namespace ChattingApp.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private UserRepository repo = null;

        public RefreshTokensController()
        {
            repo = new UserRepository();
        }

        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(repo.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [Authorize(Users = "Admin")]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}