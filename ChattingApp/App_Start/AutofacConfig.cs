using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using ChattingApp.Helpers.Translate;
using ChattingApp.Helpers.Translate.Interfaces;
using ChattingApp.Models;
using ChattingApp.Repository;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Repository;
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

            builder.RegisterType<AuthContext>().As<IAuthContext>().InstancePerLifetimeScope();

            builder.RegisterType<GoogleTranslator>().Named<ITranslator>(TranslationSource.Google.ToString());
            builder.RegisterType<BingTranslator>().Named<ITranslator>(TranslationSource.Bing.ToString());
            builder.RegisterType<YandexTranslator>().Named<ITranslator>(TranslationSource.Yandex.ToString());
            builder.RegisterType<MessageTranslator>().As<IMessageTranslator>();

            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>();
            builder.RegisterType<ChatRepository>().As<IChatRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}