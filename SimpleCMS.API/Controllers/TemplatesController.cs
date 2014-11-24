using Cerberus.Tool.TemplateEngine.Business;
using Cerberus.Tool.TemplateEngine.Common;
using Cerberus.Tool.TemplateEngine.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize]
	public class TemplatesController : ApiController
	{
		public List<TemplateViewModel> Get()
		{
			var searchParameters = this.User.IsInRole("AdministrateTemplates") ? null : new TemplateSearchParameters
				{
					CreatedByUserId = int.Parse((this.User.Identity as ClaimsIdentity).Claims.First(claim => claim.Type.Equals(ClaimTypes.Sid)).Value)
				};

			var templates = TemplateManager.TemplateService.GetTemplates(searchParameters);
			
			var result = new List<TemplateViewModel>();
			foreach (var template in templates)
			{
				result.Add(TemplateViewModel.CreateFromModel(template));
			}

			return result;
		}
	}
}
