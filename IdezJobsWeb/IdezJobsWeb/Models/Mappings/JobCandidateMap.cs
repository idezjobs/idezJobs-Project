using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class JobCandidateMap : ClassMap<JobCandidate>
	{
		public JobCandidateMap( )
		{
			Table("IdezJobs_JobCandidate");
			Id(x => x.Id, "Id");
			Map(x => x.AdditionalInformation, "AdditionalInformation").Not.Nullable( ).Length(101);

			References(x => x.JobCandidato, "DescriptionVacancy")
			.Cascade.SaveUpdate( )
			.ForeignKey("fk_JobCandidate_Vacancy")
			.Not.Nullable( );

			References(x => x.UserJobs, "UserJobs")
			.Cascade.SaveUpdate( )
			.ForeignKey("fk_JobCandidate_UserJobs")
			.Not.Nullable( );

		}
	}
}