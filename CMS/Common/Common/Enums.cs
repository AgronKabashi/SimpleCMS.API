using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Module.CMS.Common
{
	public enum PublicationType
	{
		Unpublished,
		Published,
		All
	};

	public enum OrderByType
	{
		OrderId,
		PublishStartDate,
		PublishEndDate,
		Name
	};

	public enum OrderByDirectionType
	{
		Asc,
		Desc
	}
}
