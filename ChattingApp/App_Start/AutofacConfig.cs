using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using ChattingApp.Repository;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Repository;
using ChattingApp.Service;
using Microsoft.AspNet.SignalR;

namespace ChattingApp
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            builder.RegisterType<AuthContext>().As<IAuthContext>().SingleInstance();

            builder.RegisterType<MessageRepository>().As<IMessageRepository>();
            builder.RegisterType<ChatRepository>().As<IChatRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ChatService>().As<IChatService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<MappingService>().As<IMappingService>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}