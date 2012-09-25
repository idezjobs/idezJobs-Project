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
            Map(x => x.Description, "Description").Not.Nullable( ).Not.Nullable( ).Length(50);
            Map(x => x.CompanyName, "CompanyName").Not.Nullable( ).Length(50);
            Map(x => x.JobsNumber, "JobsNumber").Not.Nullable( );
            Map(x => x.ProfileJobs, "ProfileJobs").Not.Nullable( );


        }
    }
}