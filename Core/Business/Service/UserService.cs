using Cerberus.Core.Data;
using System;

namespace Cerberus.Core.Business
{
	public class UserService
	{
		internal bool AddUser(User user)
		{
			user.Id = DataAccess.UserRepository.AddUser(user.CreateDataObject());
			return user.Id > 0;
		}
		
		public User GetUser(int userId)
		{
			var userData = DataAccess.UserRepository.GetUser(userId);

			return userData != null ? User.CreateFromDataObject(userData) : null;
		}

		public User GetUser(string username, string password)
		{
			var userData = DataAccess.UserRepository.GetUser(username, password);

			return userData != null ? User.CreateFromDataObject(userData) : null;
		}
		
		public UserCollection GetUsers()
		{
			return UserCollection.CreateFromDataObjectCollection(DataAccess.UserRepository.GetUsers());
		}

		public RoleCollection GetUserRoles(int userId)
		{
			return RoleCollection.CreateFromDataObjectCollection(DataAccess.UserRepository.GetUserRoles(userId));
		}

		internal bool UpdateUser(User user)
		{
			return DataAccess.UserRepository.UpdateUser(user.CreateDataObject());
		}

		internal void UpdateUserRoles(int userId, RoleCollection roles)
		{
			DataAccess.UserRepository.UpdateUserRoles(userId, roles.CreateDataObjectCollection());
		}

		public bool RemoveUser(int userId)
		{
			return DataAccess.UserRepository.RemoveUser(userId);
		}
	}
}
