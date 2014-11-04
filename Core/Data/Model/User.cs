using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cerberus.Core.Data
{
	public class User
	{
		public int Id
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		internal static User CreateFromData(DataRow row)
		{
			return new User
			{
				Id = Convert.ToInt32(row["UserId"]),
				UserName = row["UserName"].ToString(),
				FirstName = row["FirstName"].ToString(),
				LastName = row["LastName"].ToString()
			};
		}
	}
}
