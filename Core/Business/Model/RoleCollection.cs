using System.Collections.Generic;

namespace Cerberus.Core.Business
{
	public class RoleCollection : List<Role>
	{
		internal Data.RoleCollection CreateDataObjectCollection()
		{
			var result = new Data.RoleCollection();

			foreach (var role in this)
			{
				result.Add(role.CreateDataObject());
			}

			return result;
		}

		internal static RoleCollection CreateFromDataObjectCollection(Data.RoleCollection roles)
		{
			var result = new RoleCollection();

			foreach(var role in roles)
			{
				result.Add(Role.CreateFromDataObject(role));
			}

			return result;
		}

		internal void Save(int userId)
		{
			CoreManager.UserService.UpdateUserRoles(userId, this);
		}
	}
}
