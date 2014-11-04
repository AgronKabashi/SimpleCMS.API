namespace Cerberus.Module.CMS.Business
{
	public static class CMSManager
	{
		public static FolderService FolderService
		{
			get;
			private set;
		}

		public static ArticleService ArticleService
		{
			get;
			private set;
		}

		static CMSManager()
		{
			FolderService = new FolderService();
			ArticleService = new ArticleService();
		}
	}
}
