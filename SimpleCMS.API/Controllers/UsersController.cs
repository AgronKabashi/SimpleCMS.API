using Cerberus.Core.Business;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
    public class UsersController : ApiController
    {
		public UserCollection Get()
		{
			return CoreManager.UserService.GetUsers();
		}
    }
}
