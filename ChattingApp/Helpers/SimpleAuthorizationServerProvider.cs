using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using Microsoft.Owin.Security.OAuth;

namespace ChattingApp.Helpers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _userRepository;

        public SimpleAuthorizationServerProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException();
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (!context.TryGetBasicCredentials(out _, out _))
                context.TryGetFormCredentials(out _, out _);

            if (context.ClientId == null)
                context.SetError("invalid_clientId", "ClientId should be sent.");
            else
                context.Validated();

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var applicationUser = await _userRepository.FindAsync(context.UserName, context.Password);
            if (applicationUser == null)
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            else
                context.Validated(CreateClaimsIdentity(context, applicationUser));
                
        }

        private static ClaimsIdentity CreateClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUser applicationUser)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, applicationUser.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, applicationUser.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()));

            return identity;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));

            foreach (var property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            return Task.FromResult<object>(null);
        }
    }
}