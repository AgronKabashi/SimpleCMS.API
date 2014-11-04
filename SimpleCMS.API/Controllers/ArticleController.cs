using Cerberus.Module.CMS.Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCMS.API.Controllers
{
	public class ArticleController : ApiController
	{
		public HttpResponseMessage Options()
		{
			return Request.CreateResponse(HttpStatusCode.OK);
		}
		
		public Article Get(int id)
		{
			return CMSManager.ArticleService.GetArticle(id);
		}

		public HttpResponseMessage Put([FromBody] Article article)
		{
			var result = article != null && article.Save() ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

			return Request.CreateResponse<Article>(result, article);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = id > 0 && CMSManager.ArticleService.RemoveArticle(id) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

			return Request.CreateResponse(result, result == HttpStatusCode.OK);
		}

		public HttpResponseMessage Clone(int id)
		{
			var article = CMSManager.ArticleService.GetArticle(id);			
			var newArticleId = CMSManager.ArticleService.CloneArticle(article.Id);
			var result = newArticleId > 0 ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
			//TemplateManager.TemplateControlContentService.CloneTemplateControlContent(articleId, CMSDocumentTypeId);
			//var template = TemplateManager.TemplateService.GetDocumentTemplate(article.TemplateId, article.Id, CMSDocumentTypeId);
			//TemplateManager.TemplateControlContentService.UpdateTemplateControlContent(template, newArticleId, CMSDocumentTypeId);

			return Request.CreateResponse(result, newArticleId);
		}
	}
}
