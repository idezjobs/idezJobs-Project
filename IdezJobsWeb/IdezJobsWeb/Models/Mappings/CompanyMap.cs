using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class CompanyMap : ClassMap<Company>
	{
		public CompanyMap( )
		{
			Table("IdezJobs_Company");
			Id(x => x.Id, "Id");
			Map(x => x.Name, "Name").Not.Nullable( ).Length(51).Unique( );
			Map(x => x.Email, "Email").Not.Nullable( );
			Map(x => x.City, "City").Nullable( );
			Map(x => x.Address, "Address").Nullable( ).Length(101);
			Map(x => x.Neighborhood, "Neighborhood").Nullable( ).Length(51);
			Map(x => x.State, "State").Nullable( ).Length(51);
			Map(x => x.Operation,"Operation").Nullable().Length(51);

			HasMany(x => x.ListVacancyBusiness)
			.KeyColumn("ListVacancy")
			.Cascade.SaveUpdate( );

		}
	}
}