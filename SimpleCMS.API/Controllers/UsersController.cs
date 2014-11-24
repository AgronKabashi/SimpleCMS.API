using Cerberus.Core.Business;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize(Roles = "Administrators,AdministrateUsers")]
    public class UsersController : ApiController
    {
		public UserCollection Get()
		{
			return CoreManager.UserService.GetUsers();
		}
    }
}
