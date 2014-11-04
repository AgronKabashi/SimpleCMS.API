using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Core.Data
{
	public static class DataAccess
	{
		public static UserRepository UserRepository
		{
			get;
			private set;
		}

		static DataAccess()
		{
			UserRepository = new UserRepository();
		}
	}
}
