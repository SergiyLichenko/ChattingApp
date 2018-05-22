using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace ChattingApp.Helpers
{
    public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var value = context.Request.Query.Get("token");
            if (!string.IsNullOrEmpty(value))
                context.Token = value;

            return Task.FromResult<object>(context);
        }
    }
}