using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class ScoreMap : ClassMap<Score>
	{
		public ScoreMap( )
		{
			Table("IdezJobs_Score");
			Id(x => x.Id, "Id");
			Map(x => x.ScoreUser, "ScoreUser");

			References(x => x.ScoreProfileUser, "Profile")
				.Cascade.SaveUpdate( )
				.ForeignKey("fk_ScoreUser_Profile")
				.Nullable( );

			References(x => x.ScoreVacancy, "Vacancy")
				.Cascade.SaveUpdate( )
				.ForeignKey("fk_Vacancy_Score")
				.Nullable( );
		}
	}
}