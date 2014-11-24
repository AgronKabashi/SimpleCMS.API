using Cerberus.Core.Business;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	public class UserAuthenticationController : ApiController
	{
		//private ObjectContent<object> GenerateTicket(string username)
		//{
		//	var identity = new ClaimsIdentity(Startup.OAuthBearerOptions.AuthenticationType);
		//}

		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		//Login
		public HttpResponseMessage Put([FromBody]dynamic credentials)
		{
			var user = CoreManager.UserService.GetUser((string)credentials.username, (string)credentials.password);
			var result = user != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			if (user != null)
			{

			}

			return Request.CreateResponse(result, user);
		}

		//Logout
		public HttpResponseMessage Delete()
		{
			var result = User.Identity.IsAuthenticated ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			return Request.CreateResponse(result);
		}
	}
}
