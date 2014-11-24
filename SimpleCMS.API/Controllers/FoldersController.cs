using Cerberus.Module.CMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	[Authorize(Roles = "Administrators,AdministrateArticles,CreateArticles")]
    public class FoldersController : ApiController
    {
		public FolderCollection Get(int id)
		{
			return CMSManager.FolderService.GetFolders(id);
		}
    }
}
