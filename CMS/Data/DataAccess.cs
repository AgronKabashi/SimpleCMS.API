namespace Cerberus.Module.CMS.Data
{
	public static class DataAccess
	{
		public static FolderRepository FolderRepository
		{
			get;
			private set;
		}

		public static ArticleRepository ArticleRepository
		{
			get;
			private set;
		}

		static DataAccess()
		{
			FolderRepository = new FolderRepository();
			ArticleRepository = new ArticleRepository();
		}
	}
}