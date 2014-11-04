using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cerberus.Core.Data;

namespace Cerberus.Core.Data
{
	public class UserRepository
	{
		public int AddUser(User user)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				INSERT INTO
					[Cerberus.User] (UserName, Password, FirstName, LastName)
				VALUES
				(
					@UserName,
					LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', @Password), 2)),
					@FirstName, 
					@LastName
				);
				SELECT CAST(SCOPE_IDENTITY() AS INT)";

			SqlDbAccess.AddParameter(command, "@UserName", SqlDbType.NVarChar, user.UserName);
			SqlDbAccess.AddParameter(command, "@Password", SqlDbType.NVarChar, user.Password);

			SqlDbAccess.AddParameter(command, "@FirstName", SqlDbType.NVarChar, user.FirstName);
			SqlDbAccess.AddParameter(command, "@LastName", SqlDbType.NVarChar, user.LastName);

			return SqlDbAccess.ExecuteScalar<int>(command);
		}

		public User GetUser(int userId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					UserId,
					FirstName,
					LastName,
					UserName
				FROM
					[Cerberus.User]
				WHERE
					UserId=@UserId";

			SqlDbAccess.AddParameter(command, "@UserId", SqlDbType.Int, userId);

			var data = SqlDbAccess.ExecuteSelect(command);

			return data.Rows.Count > 0 ? User.CreateFromData(data.Rows[0]) : null;
		}

		public User GetUser(string username, string password)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					UserId,
					FirstName,
					LastName,
					UserName
				FROM
					[Cerberus.User]
				WHERE
					UserName=@UserName
					AND Password=LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', @Password), 2))";

			SqlDbAccess.AddParameter(command, "@UserName", SqlDbType.NVarChar, username);
			SqlDbAccess.AddParameter(command, "@Password", SqlDbType.NVarChar, password);

			var data = SqlDbAccess.ExecuteSelect(command);

			return data.Rows.Count > 0 ? User.CreateFromData(data.Rows[0]) : null;
		}

		public UserCollection GetUsers()
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					UserId,
					FirstName,
					LastName,
					UserName
				FROM
					[Cerberus.User]";

			return UserCollection.CreateFromData(SqlDbAccess.ExecuteSelect(command));
		}

		public bool UpdateUser(User user)
		{
			var command = SqlDbAccess.CreateTextCommand();

			var passwordUpdateQuery = string.Empty;

			if (!string.IsNullOrEmpty(user.Password))
			{
				passwordUpdateQuery = "Password=LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', @Password), 2)),";
				SqlDbAccess.AddParameter(command, "@Password", SqlDbType.NVarChar, user.Password);
			}

			command.CommandText = string.Format(@"
				UPDATE
					[Cerberus.User]
				SET
					UserName=@UserName,
					{0}
					FirstName=@FirstName,
					LastName=@LastName
				WHERE
					UserId=@UserId",
				passwordUpdateQuery);

			SqlDbAccess.AddParameter(command, "@UserId", SqlDbType.Int, user.Id);
			SqlDbAccess.AddParameter(command, "@UserName", SqlDbType.NVarChar, user.UserName);

			SqlDbAccess.AddParameter(command, "@FirstName", SqlDbType.NVarChar, user.FirstName);
			SqlDbAccess.AddParameter(command, "@LastName", SqlDbType.NVarChar, user.LastName);

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}

		public RoleCollection GetUserRoles(int userId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					UR.RoleId,
					UR.UserId,
					R.Name
				FROM
					[Cerberus.UserRoles] UR
					JOIN [Cerberus.Role] R ON UR.RoleId = R.RoleId
				WHERE
					UserId=@UserId";

			SqlDbAccess.AddParameter(command, "@UserId", SqlDbType.Int, userId);

			return RoleCollection.CreateFromData(SqlDbAccess.ExecuteSelect(command));
		}

		public void UpdateUserRoles(int userId, RoleCollection roles)
		{
			const string insertQuery = "INSERT INTO [Cerberus.UserRoles](RoleId, UserId) VALUES(@RoleId{0}, @UserId);";

			var command = SqlDbAccess.CreateTextCommand();
			var query = new System.Text.StringBuilder("DELETE FROM [Cerberus.UserRoles] WHERE UserId=@UserId;");

			if (roles.Any())
			{
				var counter = 0;
				foreach (var role in roles)
				{
					query.AppendFormat(insertQuery, counter);

					SqlDbAccess.AddParameter(command, string.Format("@RoleId{0}", counter), SqlDbType.Int, role.Id);

					counter++;
				}
			}

			SqlDbAccess.AddParameter(command, "@UserId", SqlDbType.Int, userId);

			command.CommandText = query.ToString();

			SqlDbAccess.ExecuteNonQuery(command);
		}

		public bool RemoveUser(int userId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				DELETE FROM
					[Cerberus.User]
				WHERE
					UserId=@UserId";

			SqlDbAccess.AddParameter(command, "@UserId", SqlDbType.Int, userId);

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}
	}
}
