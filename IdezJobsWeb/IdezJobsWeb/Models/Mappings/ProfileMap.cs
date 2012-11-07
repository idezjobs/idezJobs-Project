using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace IdezJobsWeb.Models.Mappings
{
	public class ProfileMap : ClassMap<Profile>
	{
		public ProfileMap( )
		{
			Table("IdezJobs_Profile");
			Id(x => x.Code, "Code");
			Map(x => x.Id, "Id");
			Map(x => x.FirstName, "FirstName");
			Map(x => x.LastName, "LastName");
			Map(x => x.PictureUrl, "PictureUrl");
			Map(x => x.PublicUrl, "PublicUrl");
			Map(x => x.Headline, "Headline");
			Map(x => x.Interests,"Interests");
			Map(x => x.EmailAddress, "EmailAddress");			
			Map(x => x.IdUser,"IdUser");

		}
	}
}