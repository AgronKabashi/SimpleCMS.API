using Cerberus.Tool.TemplateEngine.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize(Roles = "Administrators,AdministrateTemplates,CreateTemplates")]
	public class TemplateController : ApiController
	{
		[AllowAnonymous]
		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[AllowAnonymous]
		public Template Get(int id)
		{
			return TemplateManager.TemplateService.GetTemplate(id);
		}
		[AllowAnonymous]
		public HttpResponseMessage Put(Template template)
		{
			var result = template != null && template.Save(0, false) ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, template);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = TemplateManager.TemplateService.RemoveTemplate(id) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result);
		}
	}
}
