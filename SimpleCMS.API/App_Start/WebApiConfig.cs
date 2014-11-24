using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SimpleCMS.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "ArticleStartPage",
				routeTemplate: "api/Article/StartPage",
				defaults: new { controller = "Article" }
			);

			config.Routes.MapHttpRoute(
				name: "UserAuthentication",
				routeTemplate: "api/UserAuthentication",
				defaults: new { controller = "UserAuthentication" }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultActions",
				routeTemplate: "api/{controller}/{id}/{action}",
				defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
			);

			config.Formatters.Clear();
			config.Formatters.Add(new JsonMediaTypeFormatter());

			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			config.EnableCors();

			#if DEBUG
			config.EnableSystemDiagnosticsTracing();
			#endif
		}
	}
}
