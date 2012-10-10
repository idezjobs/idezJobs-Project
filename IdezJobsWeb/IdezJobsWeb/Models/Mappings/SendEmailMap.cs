using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class SendEmailMap : ClassMap<SendEmail>
	{
		public SendEmailMap( )
		{
			Table("IdezJobs_SendEmail");
			Id(x => x.Id, "Id");
			Map(x => x.NameSendEmail, "NameSendEmail").Not.Nullable( ).Length(50);

			HasMany(x => x.EmailList)
			.KeyColumn("EmailList")
			.Cascade.SaveUpdate();
		}

	}
}