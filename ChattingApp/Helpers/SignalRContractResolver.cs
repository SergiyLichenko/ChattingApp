using System;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace ChattingApp.Helpers
{
    public class SignalRContractResolver : IContractResolver
    {
        public JsonContract ResolveContract(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.Assembly.Equals(typeof(Connection).Assembly) ?
                new DefaultContractResolver().ResolveContract(type) :
                new CamelCasePropertyNamesContractResolver().ResolveContract(type);
        }
    }
}