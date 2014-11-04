using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Module.CMS.Data
{
	public class ArticleCollection : List<Article>
	{
		internal static ArticleCollection CreateFromData(DataTable data)
		{
			var list = new ArticleCollection();

			foreach (DataRow row in data.Rows)
			{
				list.Add(Article.CreateFromData(row));
			}

			return list;
		}
	}
}
