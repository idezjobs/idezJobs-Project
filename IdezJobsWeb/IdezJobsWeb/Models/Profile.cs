using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdezJobsWeb.Models
{
	public class Profile
	{
		public virtual long Code { get; set; }
		public virtual string Id { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string PictureUrl { get; set; }
		public virtual string PublicUrl { get; set; }
		public virtual string Headline { get; set; }
		public virtual string Industry { get; set; }




	}
}