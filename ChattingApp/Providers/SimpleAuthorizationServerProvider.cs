using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ChattingApp.Repository.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace ChattingApp.Providers
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

            var user = await _userRepository.FindAsync(context.UserName, context.Password);
            if (user == null)
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            else
                context.Validated(CreateAuthenticatinTicket(context));
        }

        private static AuthenticationTicket CreateAuthenticatinTicket(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var props = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { "as:client_id", context.ClientId ?? string.Empty },
                    {"UserName", context.UserName }
                });

            var ticket = new AuthenticationTicket(identity, props);
            return ticket;
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