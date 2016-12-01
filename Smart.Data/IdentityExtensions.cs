using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Repository
{
    public static class IdentityExtensions
    {
        public static string GetIsPrivate(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("IsPrivate");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetChats(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Chats");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
