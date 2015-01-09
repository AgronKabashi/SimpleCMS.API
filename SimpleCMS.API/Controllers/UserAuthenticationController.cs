using Cerberus.Core.Business;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SimpleCMS.API.Controllers
{
	public class UserAuthenticationController : ApiController
	{
		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		//Login
		public HttpResponseMessage Put([FromBody]dynamic credentials)
		{
			var user = CoreManager.UserService.GetUser((string)credentials.username, (string)credentials.password);
			var result = user != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, user);
		}

		//Logout
		public HttpResponseMessage Delete()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
