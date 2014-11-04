using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Core.Business
{
	public static class CoreManager
	{
		public static UserService UserService
		{
			get;
			private set;
		}

		static CoreManager()
		{
			UserService = new UserService();
		}
	}
}
