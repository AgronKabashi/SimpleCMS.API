using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cerberus.Core.Data
{
	public class UserCollection : List<User>
	{
		internal static UserCollection CreateFromData(DataTable data)
		{
			var result = new UserCollection();

			foreach (DataRow row in data.Rows)
			{
				result.Add(User.CreateFromData(row));
			}

			return result;
		}
	}
}
