using System;
using FizzWare.NBuilder.Dates;
using System.Collections.Generic;

namespace BudgetUp.Model
{
	public class Period
	{
		public Period (DateTime from, DateTime to)
		{
			this.From = from;
			this.To = to;
		}

		public DateTime From { get; private set;}

		public DateTime To { get; private set;}

		

		public override string ToString ()
		{
			return string.Format ("{0} - {1}", From.ToShortDateString(), 
			                      			   To.ToShortDateString());
		}
	}
}

