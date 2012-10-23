using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class StatusMap : ClassMap<Status>
	{
		public StatusMap( )
		{
			Table("IdezJobs_Status");
			Id(x => x.Code, "Code");
			Map(x => x.Description, "Description").Not.Nullable( ).Length(40);
		}
	}
}