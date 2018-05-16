using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ChattingApp.Controllers;
using ChattingApp.Repository;
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

            builder.RegisterType<MessageHub>().ExternallyOwned();

          
            builder.Register(x => new UserService(new UserRepository(), new MappingService())).As<IUserService>();
            builder.Register(x => new ChatsService(new ChatsRepository(new AuthContext()), new UserRepository(), 
                new MappingService(), new UserService(new UserRepository(), new MappingService()))).As<IChatsService>();

            builder.Register(x => new MessageService(new MessageRepository(new AuthContext()), new MappingService(), new ChatsRepository(new AuthContext()))).As<IMessageService>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}