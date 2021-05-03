using System;
using FizzWare.NBuilder.Dates;
using System.Collections.Generic;

namespace BudgetUp.Model
{
	public abstract class ForEach
	{
		public ForEach ()
		{
			this.StartDay = The.Year (DateTime.Now.Year).On.January.The1st;
		}

		public static ForEach Month { get { return new Month (); } }

		public static ForEach Day { get { return new Day (); } }

		protected DateTime StartDay { get; set; } 

		public ForEach StartingFrom (DateTime start)
		{
			this.StartDay = start;
			return this;
		}

		public abstract List<Period> GetPeriods ();
	}

	public class Day : ForEach {

		public override List<Period> GetPeriods ()
		{
			List<Period> periods = new List<Period> ();

			var start = StartDay;

			while (start != StartDay.AddYears(1)) {
				Period period = new Period (start, start);
				start = start.AddDays (1);

				periods.Add (period);
			} 

			return periods;
		}

	}

	public class Month : ForEach {

		public override List<Period> GetPeriods ()
		{
			List<Period> periods = new List<Period> ();

			var start = StartDay;

			for (int i = 0; i < 12; i++) {
				Period period = new Period (start, start.AddMonths (1).AddDays(-1));
				start = period.To.AddDays(1);
				
				periods.Add (period);
			}

			return periods;

		}
	}

}

