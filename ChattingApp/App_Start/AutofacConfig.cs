using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ChattingApp.Controllers;
using ChattingApp.Repository;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Repository;
using ChattingApp.Service;

namespace ChattingApp
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<AuthContext>().As<IAuthContext>();

            builder.RegisterType<ChatRepository>().As<IChatRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MessageHub>().ExternallyOwned();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ChatService>().As<IChatService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<MappingService>().As<IMappingService>();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}