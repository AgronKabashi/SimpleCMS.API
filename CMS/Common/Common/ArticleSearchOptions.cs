using System;
namespace Cerberus.Module.CMS.Common
{
	public class ArticleSearchOptions
	{
		public PublicationType Publication
		{
			get;
			set;
		}

		public OrderByType OrderBy
		{
			get;
			set;
		}

		public OrderByDirectionType OrderByDirection
		{
			get;
			set;
		}

		public ArticleSearchOptions()
		{
			this.Publication = PublicationType.All;
			this.OrderBy = OrderByType.OrderId;
			this.OrderByDirection = OrderByDirectionType.Asc;
		}
	}
}