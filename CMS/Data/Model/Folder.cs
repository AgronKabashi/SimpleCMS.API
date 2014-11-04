using System;
using System.Data;

namespace Cerberus.Module.CMS.Data
{
	public class Folder
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

		public string SEOName
		{
			get;
			set;
		}

		public int ParentId
		{
			get;
			set;
		}

		internal static Folder CreateFromData(DataRow row)
		{
			return new Folder
			{
				Id = Convert.ToInt32(row["FolderId"]),
				Name = row["Name"].ToString(),
				SEOName = row["SEOName"].ToString(),
				ParentId = row.IsNull("ParentId") ? 0 : Convert.ToInt32(row["ParentId"])
			};
		}
	}
}
