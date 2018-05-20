using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using ChattingApp;
using ChattingApp.Providers;
using ChattingApp.Repository;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Repository;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(StartUp))]
namespace ChattingApp
{
    public class StartUp
    {
        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            var userRepository = config.DependencyResolver
                .GetRootLifetimeScope().Resolve<IUserRepository>();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                Provider = new SimpleAuthorizationServerProvider(userRepository),
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuthContext>());
            AutofacConfig.Register(config);
            AutoMapperConfig.Register();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register(config);
            ConfigureOAuth(app, config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR("/signalr", new HubConfiguration());
            app.UseWebApi(config);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
        }
    }
}