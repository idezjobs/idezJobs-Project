using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class EmailMap : ClassMap<Email>
	{
		public EmailMap( )
		{
			Table("IdezJobs_Email");
			Id(x => x.Id, "Id");
			Map(x => x.Email, "Email").Not.Nullable( );
			Map(x => x.Subject, "Subject").Nullable( ).Default("Sem Assunto");
			Map(x => x.FileAnexo, "Anexo").Nullable( );
		}
	}
}