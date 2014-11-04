using System.Runtime.Serialization;

namespace Cerberus.Core.Business
{
	[DataContract]
	public class User
	{
		private RoleCollection roles = null;

		[DataMember(IsRequired = true)]
		public int Id
		{
			get;
			internal set;
		}

		[DataMember]
		public string UserName
		{
			get;
			set;
		}

		[DataMember]
		public string Password
		{
			get;
			set;
		}

		[DataMember]
		public string FirstName
		{
			get;
			set;
		}

		[DataMember]
		public string LastName
		{
			get;
			set;
		}

		[DataMember]
		public RoleCollection Roles
		{
			get
			{
				if (this.roles == null)
				{
					this.roles = CoreManager.UserService.GetUserRoles(this.Id);
				}

				return this.roles;
			}

			private set 
			{
				this.roles = value;
			}
		}

		public bool Save()
		{
			if (this.Id > 0)
			{
				CoreManager.UserService.UpdateUser(this);
			}
			else
			{
				CoreManager.UserService.AddUser(this);
			}

			this.Roles.Save(this.Id);

			return true;
		}

		internal Data.User CreateDataObject()
		{
			return new Data.User
			{
				Id = this.Id,
				UserName = this.UserName,
				Password = this.Password,
				FirstName = this.FirstName,
				LastName = this.LastName
			};
		}

		internal static User CreateFromDataObject(Data.User user)
		{
			return new User
			{
				Id = user.Id,
				UserName = user.UserName,
				FirstName = user.FirstName,
				LastName = user.LastName
			};
		}
	}
}
