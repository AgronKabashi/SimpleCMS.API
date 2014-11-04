using System;

namespace Cerberus.Module.CMS.Common
{
	public class Publication
	{
		public bool IsPublished
		{
			get;
			set;
		}

		public DateTime PublishStartDate
		{
			get;
			set;
		}

		public DateTime PublishEndDate
		{
			get;
			set;
		}

		public Publication()
		{
			this.PublishStartDate = new DateTime(1901, 1, 1);
			this.PublishEndDate = new DateTime(2050, 1, 1);
		}
	}
}
