using System;
using System.Linq;

namespace BudgetUp.Model
{
	public class Expense : Movement
	{
		private double expense;

		public override double AbsoluteValue {
			get { return expense; }
			set { this.expense = value; }
		}

		public override double Value {
			get { return -AbsoluteValue; }
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

		public Expense (double value, DateTime dateTime, params string[] tags)
			: this("", value, dateTime, tags)
		{
		}

		public Expense (string description, double value, DateTime dateTime, params string[] tags)
		{
			if (value < 0)
				throw new Exception ("Expense cannot be negative");

			this.AbsoluteValue = value;
			this.Tags = tags;
			this.Date = dateTime;
			this.Description = description;

		}

	}
}

