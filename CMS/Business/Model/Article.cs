using Cerberus.Module.CMS.Common;
using System.Runtime.Serialization;

namespace Cerberus.Module.CMS.Business
{
	[DataContract]
	public class Article
	{
		[DataMember(IsRequired = true)]
		public int Id
		{
			get;
			internal set;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}

		[DataMember]
		public string Description
		{
			get;
			set;
		}

		[DataMember]
		public string SEOName
		{
			get;
			set;
		}

		[DataMember(IsRequired = true)]
		public int FolderId
		{
			get;
			internal set;
		}

		[DataMember]
		public int TemplateId
		{
			get;
			set;
		}

		[DataMember]
		public string Image
		{
			get;
			set;
		}

		[DataMember]
		public string Thumbnail
		{
			get;
			set;
		}

		[DataMember]
		public Publication Publication
		{
			get;
			set;
		}

		public Article()
		{
			this.Publication = new Publication();
		}

		public bool Save()
		{
			var result = false;

			result = this.Id > 0 ? CMSManager.ArticleService.UpdateArticle(this) : CMSManager.ArticleService.AddArticle(this);

			return true;
		}

		internal Data.Article CreateDataObject()
		{
			return new Data.Article
			{
				Id = this.Id,
				Name = this.Name,
				Description = this.Description,
				SEOName = this.SEOName,
				FolderId = this.FolderId,
				TemplateId = this.TemplateId,
				Image = this.Image,
				Thumbnail = this.Thumbnail,
				Publication = this.Publication
			};

		}

		internal static Article CreateFromDataObject(Data.Article article)
		{
			return new Article
			{
				Id = article.Id,
				Name = article.Name,
				Description = article.Description,
				SEOName = article.SEOName,
				FolderId = article.FolderId,
				TemplateId = article.TemplateId,
				Image = article.Image,
				Thumbnail = article.Thumbnail,
				Publication = article.Publication
			};
		}
	}
}
