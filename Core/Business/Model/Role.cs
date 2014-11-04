using System.Runtime.Serialization;

namespace Cerberus.Core.Business
{
	[DataContract]
	public class Role
	{
		[DataMember(IsRequired=true)]
		public int Id
		{
			get;
			private set;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}

		internal Data.Role CreateDataObject()
		{
			return new Data.Role
			{
				Id = this.Id,
				Name = this.Name
			};
		}

		internal static Role CreateFromDataObject(Data.Role role)
		{
			return new Role
			{
				Id = role.Id,
				Name = role.Name
			};
		}
	}
}
