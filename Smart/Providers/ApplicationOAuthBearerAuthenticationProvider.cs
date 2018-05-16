using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace ChattingApp.Providers
{
    public class ApplicationOAuthBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

           
            var tokenCookie = context.OwinContext.Request.Cookies["BearerToken"];
            if (!string.IsNullOrEmpty(tokenCookie))
            {
                context.Token = tokenCookie;
            }
                

            return Task.FromResult<object>(null);
        }
    }
}