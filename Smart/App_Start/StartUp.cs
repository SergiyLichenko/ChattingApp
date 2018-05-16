using System;
using Smart;
using Smart.Providers;

[assembly: OwinStartup(typeof(StartUp))]
namespace Smart
{
    public class StartUp
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            //For Google and Facebook part
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions); /*new OAuthBearerAuthenticationOptions()
            {
                Provider = new ApplicationOAuthBearerAuthenticationProvider(),
            });*/


            //Google
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "821228275438-lrulvmn03d9br2qbp3tkf44hjlev9tvm.apps.googleusercontent.com",
                ClientSecret = "WFOwoIOhtlGCDmI3Vo3gjohS",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //Facebook
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "185013055265833",
                AppSecret = "2e507a97f0b5cc8e850a9ef68e3e084c",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);
        }

        public void Configuration(IAppBuilder app)
        {


            Database.SetInitializer(new MyInitializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, DBConfiguration>());

            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        

            AutofacConfig.Register(config);
            AutoMapperConfig.Register();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            app.MapSignalR("/signalr", new HubConfiguration());

        }


    }
}