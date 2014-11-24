using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(SimpleCMS.API.Startup))]
namespace SimpleCMS.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{	
			var config = new HttpConfiguration();

			WebApiConfig.Register(config);

			app.UseCors(CorsOptions.AllowAll);
			this.ConfigureOAuth(app);
			app.UseWebApi(config);
		}

		public void ConfigureOAuth(IAppBuilder app)
		{
			var options = new OAuthAuthorizationServerOptions
				{
					AllowInsecureHttp = true,
					TokenEndpointPath = new PathString("/api/token"),
					AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
					Provider = new SimpleAuthorizationServerProvider()
				};

			app.UseOAuthAuthorizationServer(options);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}