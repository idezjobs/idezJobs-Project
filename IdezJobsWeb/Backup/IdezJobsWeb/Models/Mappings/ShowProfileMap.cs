using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class ShowProfileMap : ClassMap<ShowProfile>
	{
		public ShowProfileMap( )
		{
			Table("IdezJobs_ShowProfile");
			Id(x => x.Id, "Id");
			Map(x => x.Name, "Name").Nullable( );
			Map(x => x.Surname, "Surname").Nullable( );
			Map(x => x.Email, "Email").Nullable( );


		}
	}
}