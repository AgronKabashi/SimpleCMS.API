using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace SimpleCMS.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "ArticleStartPage",
				routeTemplate: "api/Article/StartPage",
				defaults: new { controller = "Article" }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultActions",
				routeTemplate: "api/{controller}/{id}/{action}",
				defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
			);

			// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
			// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
			// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
			//config.EnableQuerySupport();

			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			config.EnableSystemDiagnosticsTracing();

			config.Formatters.Clear();
			config.Formatters.Add(new JsonMediaTypeFormatter());

			//config.EnableCors();
		}
	}
}
