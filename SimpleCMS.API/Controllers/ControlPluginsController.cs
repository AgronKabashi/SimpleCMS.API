using Cerberus.Tool.TemplateEngine.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	public class ControlPluginsController : ApiController
	{
		public ControlPluginCollection Get()
		{
			return TemplateManager.ControlPluginService.GetControlPlugins();
		}
	}
}
