using Cerberus.Core.Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize(Roles = "Administrators,AdministrateUsers")]
    public class UserController : ApiController
    {
		[AllowAnonymous]
		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[AllowAnonymous]
		public User Get(int id)
		{
			return CoreManager.UserService.GetUser(id);
		}

		[AllowAnonymous]
		public User Get(string username, string password)
		{
			return CoreManager.UserService.GetUser(username, password);
		}

		public HttpResponseMessage Put([FromBody] User user)
		{
			var result = user != null && user.Save() ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse<User>(result, user);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = id > 0 && CoreManager.UserService.RemoveUser(id) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, result == HttpStatusCode.OK);
		}
    }
}
