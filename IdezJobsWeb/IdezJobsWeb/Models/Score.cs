using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdezJobsWeb.Models
{
	public class Score
	{
		public virtual long Id { get; set; }
		public virtual int ScoreUser { get; set; }
		public virtual Profile ScoreProfileUser { get; set; }
		public virtual Vacancy ScoreVacancy { get; set; }

	}
}