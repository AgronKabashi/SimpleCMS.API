using Cerberus.Module.CMS.Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize(Roles = "Administrators,AdministrateArticles")]
	public class FolderController : ApiController
	{
		[AllowAnonymous]
		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[AllowAnonymous]
		public Folder Get(int id)
		{
			return CMSManager.FolderService.GetFolder(id);
		}

		public HttpResponseMessage Put([FromBody] Folder folder)
		{
			var result = folder != null && folder.Save() ? System.Net.HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse<Folder>(result, folder);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = id > 0 && CMSManager.FolderService.RemoveFolder(id) ? System.Net.HttpStatusCode.OK : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, result == HttpStatusCode.OK);
		}
	}
}
