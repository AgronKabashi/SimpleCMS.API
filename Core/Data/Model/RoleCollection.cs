using System.Collections.Generic;
using System.Data;

namespace Cerberus.Core.Data
{
	public class RoleCollection : List<Role>
	{
		internal static RoleCollection CreateFromData(DataTable data)
		{
			var result = new RoleCollection();

			foreach (DataRow row in data.Rows)
			{
				result.Add(Role.CreateFromData(row));
			}

			return result;
		}
	}
}
