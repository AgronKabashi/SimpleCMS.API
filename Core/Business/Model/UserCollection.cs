using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Core.Business
{
	public class UserCollection : List<User>
	{
		internal static UserCollection CreateFromDataObjectCollection(Data.UserCollection users)
		{
			var result = new UserCollection();

			foreach(var user in users)
			{
				result.Add(User.CreateFromDataObject(user));
			}

			return result;
		}
	}
}
