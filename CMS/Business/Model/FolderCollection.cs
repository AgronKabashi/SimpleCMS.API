using System.Collections.Generic;

namespace Cerberus.Module.CMS.Business
{
	public class FolderCollection : List<Folder>
	{
		internal Data.FolderCollection CreateDataObjectCollection()
		{
			var result = new Data.FolderCollection();

			foreach (var folder in this)
			{
				result.Add(folder.CreateDataObject());
			}

			return result;
		}

		internal static FolderCollection CreateFromDataObjectCollection(Data.FolderCollection folders)
		{
			var result = new FolderCollection();

			foreach (var folder in folders)
			{
				result.Add(Folder.CreateFromDataObject(folder));
			}

			return result;
		}
	}
}
