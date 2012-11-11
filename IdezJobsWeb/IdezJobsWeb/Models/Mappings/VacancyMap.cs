using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class VacancyMap : ClassMap<Vacancy>
	{
		public VacancyMap( )
		{
			Table("IdezJobs_Vacancy");
			Id(x => x.Id, "Id");
			Map(x => x.RegistrionDate, "RegistrionDate").Not.Nullable( );
			Map(x => x.RegistrationDeadline, "RegistrationDeadline").Not.Nullable( );
			Map(x => x.Description, "Description").Not.Nullable( ).Not.Nullable( ).Length(200);			
			Map(x => x.JobsNumber, "JobsNumber").Not.Nullable( );
			Map(x => x.Benefits, "Beneficios").Nullable( );
			Map(x => x.OfficeHours, "OfficeHours").Nullable( );

			References(x => x.ProfileVacancy, "ProfileVacancy")
			.Cascade.SaveUpdate( )
			.ForeignKey("fk_Vacancy_ProfileVacancy")
			.Not.Nullable( );


			References(x => x.Status, "Status")
			.Cascade.SaveUpdate( )
			.ForeignKey("fk_Vacancy_Status")
			.Not.Nullable( );

			References(x => x.CompanyName,"Company")
			.Cascade.SaveUpdate()
			.ForeignKey("fk_Vacancy_Company")
			.Nullable();


		}
	}
}