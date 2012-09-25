using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class DownloadCurriculumMap : ClassMap<DownloadCurriculum>
	{
		public DownloadCurriculumMap( )
		{
			Table("IdezJobs_DownloadCurriculo");
			Id(x => x.Id, "Id");
			Map(x => x.Token, "Token").Nullable( );

		}
	}
}