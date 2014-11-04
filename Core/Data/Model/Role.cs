using System;
using System.Data;

namespace Cerberus.Core.Data
{
	public class Role
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

		internal static Role CreateFromData(DataRow row)
		{
			return new Role
			{
				Id = Convert.ToInt32(row["RoleId"]),
				Name = row["Name"].ToString()
			};
		}
	}
}
