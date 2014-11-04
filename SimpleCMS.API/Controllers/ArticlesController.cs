using Cerberus.Module.CMS.Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
    public class ArticlesController : ApiController
    {
		public ArticleCollection Get(int id)
		{
			return CMSManager.ArticleService.GetArticles(id, new Cerberus.Module.CMS.Common.ArticleSearchOptions());
		}
    }
}
