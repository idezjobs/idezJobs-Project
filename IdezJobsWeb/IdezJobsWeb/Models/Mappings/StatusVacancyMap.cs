using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class StatusVacancyMap : ClassMap<StatusVacancy>
	{
		public StatusVacancyMap( )
		{
			Table("IdezJobs_StatusVacancy");
			Id(x => x.Id,"Id");
			Map(x => x.Status,"StatusVacancy").Not.Nullable().Length(55).Unique();
		}
	}
}