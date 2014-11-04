using Cerberus.Tool.TemplateEngine.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
    public class TemplateController : ApiController
    {
		public Template Get(int id)
		{
			return TemplateManager.TemplateService.GetTemplate(id);
		}

		public HttpResponseMessage Put(Template template)
		{
			var result = template != null && template.Save(0) ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, template);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = TemplateManager.TemplateService.RemoveTemplate(id) ? HttpStatusCode.OK: HttpStatusCode.BadRequest;

			return Request.CreateResponse(result);
		}
    }
}
