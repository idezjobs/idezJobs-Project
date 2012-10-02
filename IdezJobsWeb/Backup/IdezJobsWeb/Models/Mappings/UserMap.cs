using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class UserMap : ClassMap<User>
	{
		public UserMap( )
		{
			Table("IdezJobs_User");
			Id(x => x.Id, "Id");
			Map(x => x.Name, "Name").Not.Nullable( ).Length(50);
			Map(x => x.Email, "Email").Not.Nullable( );
			Map(x => x.DateRegister, "DateRegister").Not.Nullable( );
			Map(x => x.Type, "Type").Not.Nullable( );
			Map(x => x.Token, "Token").Nullable( );

		}
	}
}