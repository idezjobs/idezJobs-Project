using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class ProfileVacancyMap : ClassMap<ProfileVacancy>
	{
		public ProfileVacancyMap( )
		{
			Table("IdezJobs_ProfileVacancy");
			Id(x => x.Id, "Id");
			Map(x => x.Myprofile, "MyProfile").Not.Nullable( ).Length(60);
		}
	}
}