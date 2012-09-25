using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdezJobsWeb.Models
{
	public class DownloadCurriculum
	{
		public virtual long Id { get; set; }  
		public virtual string Token { get; set; }
	}
}