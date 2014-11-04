using Cerberus.Module.CMS.Common;
using Cerberus.Module.CMS.Data;

namespace Cerberus.Module.CMS.Business
{
	public class ArticleService
	{
		internal bool AddArticle(Article article)
		{
			article.Id = DataAccess.ArticleRepository.AddArticle(article.CreateDataObject());
			return article.Id > 0;
		}

		public Article GetArticle(int articleId)
		{
			var article = DataAccess.ArticleRepository.GetArticle(articleId);
			return article != null ? Article.CreateFromDataObject(article) : null;
		}

		public ArticleCollection GetArticles(int folderId, ArticleSearchOptions searchOptions)
		{
			searchOptions = searchOptions ?? new ArticleSearchOptions();
			return ArticleCollection.CreateFromDataObjectCollection(DataAccess.ArticleRepository.GetArticles(folderId, searchOptions));
		}

		internal bool UpdateArticle(Article article)
		{
			return DataAccess.ArticleRepository.UpdateArticle(article.CreateDataObject());
		}

		public bool RemoveArticle(int articleId)
		{
			return DataAccess.ArticleRepository.RemoveArticle(articleId);
		}

		public Article GetStartPage()
		{
			var article = DataAccess.ArticleRepository.GetStartPage();
			return article != null ? Article.CreateFromDataObject(article) : null;
		}

		public int CloneArticle(int articleId)
		{
			return DataAccess.ArticleRepository.CloneArticle(articleId);
		}
	}
}
