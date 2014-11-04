using System.Collections.Generic;
using System.Data;

namespace Cerberus.Module.CMS.Data
{
	public class FolderCollection : List<Folder>
	{
		internal static FolderCollection CreateFromData(DataTable data)
		{
			var list = new FolderCollection();

			foreach (DataRow row in data.Rows)
			{
				list.Add(Folder.CreateFromData(row));
			}

			return list;
		}
	}
}
