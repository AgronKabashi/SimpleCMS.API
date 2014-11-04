using System.Runtime.Serialization;
namespace Cerberus.Module.CMS.Business
{
	[DataContract]
	public class Folder
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
		public string SEOName
		{
			get;
			set;
		}

		[DataMember]
		public int ParentId
		{
			get;
			set;
		}

		public bool Save()
		{
			var result = false;

			result = this.Id > 0 ? CMSManager.FolderService.UpdateFolder(this) : CMSManager.FolderService.AddFolder(this);

			return true;
		}

		internal Data.Folder CreateDataObject()
		{
			return new Data.Folder
			{
				Id = this.Id,
				Name = this.Name,
				SEOName = this.SEOName,
				ParentId = this.ParentId
			};
		}

		internal static Folder CreateFromDataObject(Data.Folder folder)
		{
			return new Folder
			{
				Id = folder.Id,
				Name = folder.Name,
				SEOName = folder.SEOName,
				ParentId = folder.ParentId
			};
		}
	}
}
