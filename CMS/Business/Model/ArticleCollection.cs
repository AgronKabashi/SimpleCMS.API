using System.Collections.Generic;

namespace Cerberus.Module.CMS.Business
{
	public class ArticleCollection : List<Article>
	{
		internal Data.ArticleCollection CreateDataObjectCollection()
		{
			var result = new Data.ArticleCollection();

			foreach (var folder in this)
			{
				result.Add(folder.CreateDataObject());
			}

			return result;
		}

		internal static ArticleCollection CreateFromDataObjectCollection(Data.ArticleCollection articles)
		{
			var result = new ArticleCollection();

			foreach (var article in articles)
			{
				result.Add(Article.CreateFromDataObject(article));
			}

			return result;
		}
	}
}
