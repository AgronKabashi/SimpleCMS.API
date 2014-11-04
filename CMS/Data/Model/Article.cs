using Cerberus.Module.CMS.Common;
using System;
using System.Data;
namespace Cerberus.Module.CMS.Data
{
	public class Article
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string SEOName
		{
			get;
			set;
		}

		public int FolderId
		{
			get;
			set;
		}

		public int TemplateId
		{
			get;
			set;
		}

		public string Image
		{
			get;
			set;
		}

		public string Thumbnail
		{
			get;
			set;
		}

		public Publication Publication
		{
			get;
			set;
		}

		internal static Article CreateFromData(DataRow row)
		{
			return new Article
			{
				Id = Convert.ToInt32(row["ArticleId"]),
				Name = row["Name"].ToString(),
				Description = row["Description"].ToString(),
				SEOName = row["SEOName"].ToString(),
				FolderId = row.IsNull("FolderId") ? 0 : Convert.ToInt32(row["FolderId"]),
				TemplateId = Convert.ToInt32(row["TemplateId"]),
				Image = row["Image"].ToString(),
				Thumbnail = row["Thumbnail"].ToString(),
				Publication = new Publication
				{
					IsPublished = Convert.ToBoolean(row["IsPublished"]),
					PublishStartDate = (DateTime)row["PublishStartDate"],
					PublishEndDate = (DateTime)row["PublishEndDate"]
				}
			};
		}
	}
}
