using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class AdministratorMap : ClassMap<Administrator>
	{
		public AdministratorMap( )
		{
			//a
			Table("IdezJobs_Administrator");
			Id(x => x.Id, "Id");
			Map(x => x.Name, "Name").Not.Nullable( ).Length(51);
			Map(x => x.Email, "Email").Not.Nullable( );
			Map(x => x.Password, "Password").Not.Nullable( );
			Map(x => x.Type, "Type").Not.Nullable( ).Length(51);
		}
	}
}