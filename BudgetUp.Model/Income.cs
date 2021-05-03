using System;
using System.Linq;

namespace BudgetUp.Model
{
	public class Income : Movement
	{
		private double value;


		public override double AbsoluteValue {
			get { return value; }
			set { this.value = value; }
		}

		public override double Value {
			get { return AbsoluteValue; }
		}

		public override DateTime Date {
			get;
			set;
		}

		public override string[] Tags {
			get;
			set;
		}

		public override string Description {
			get;
			set;
		}

		public Income(double income, DateTime dateTime, params string[] tags) 
			: this("", income, dateTime, tags)
		{
		}

		public Income(string description, double income, DateTime dateTime, params string[] tags) {
			if (income < 0)
				throw new Exception ("Income cannot be negative");

			AbsoluteValue = income;
			Tags = tags;
			Date = dateTime;
			Description = description;
		}

	}
}

