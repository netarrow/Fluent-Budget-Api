using System;
using BudgetUp.Model;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace BudgetUp.Model
{
	public abstract class Movement : IMovement
	{
		#region IEqualityComparer implementation

		public bool Equals (IMovement x, IMovement y)
		{
			return x.Equals (y);
		}

		public int GetHashCode (IMovement obj)
		{
			return obj.GetHashCode ();
		}

		#endregion

		public override bool Equals(Object obj) {
			IMovement a = (IMovement) obj;
			return a.Value == this.Value && 
				a.Date == this.Date && Enumerable.SequenceEqual (this.Tags, a.Tags);
		}

		public override int GetHashCode ()
		{
			return GetHashCode (this);
		}

		public static IMovement Parse (string line)
		{
			string[] fields = line.Split (',');

			if (fields [0] != "Banca")
				return null;

			double value = Double.Parse (fields [4].Replace("€", "").Replace("\"", "").Trim());

			DateTime date = DateTime.ParseExact (fields [1], 
			                                     new string[] {"M/d/yyyy", "dd MMM yy"}, 
												 new CultureInfo("it-IT"),
											     DateTimeStyles.None);

			string[] tags = fields [2].Split(' ');

			if (value <= 0)
				return new Expense (fields [3], Math.Abs(value), date, tags);
			else
				return new Income (fields [3], Math.Abs(value), date, tags);
		}

		#region IMovement implementation

		public abstract double AbsoluteValue { get; set; }

		public abstract double Value { get;}

		public abstract string[] Tags { get; set; }

		public abstract string Description { get; set;}

		public abstract DateTime Date { get; set; }

		#endregion
	}
}

