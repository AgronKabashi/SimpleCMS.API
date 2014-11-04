using Cerberus.Tool.TemplateEngine.Business;
using Cerberus.Tool.TemplateEngine.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	public class TemplateInfoController : ApiController
	{
		public TemplateViewModel Get(int id)
		{
			return TemplateViewModel.CreateFromModel(TemplateManager.TemplateService.GetTemplate(id));
		}

		public HttpResponseMessage Put(Template template)
		{
			var result = template != null && template.Save(0) ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, template);
		}
	}
}
