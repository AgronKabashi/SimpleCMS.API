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
	public class TemplatesController : ApiController
	{
		public List<TemplateViewModel> Get()
		{
			var templates = TemplateManager.TemplateService.GetTemplates();
			
			var result = new List<TemplateViewModel>();
			foreach (var template in templates)
			{
				result.Add(TemplateViewModel.CreateFromModel(template));
			}

			return result;
		}
	}
}
