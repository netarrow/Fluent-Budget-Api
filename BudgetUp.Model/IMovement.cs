using System;
using System.Collections.Generic;

namespace BudgetUp.Model
{
	public interface IMovement : IEqualityComparer<IMovement>
	{
		double AbsoluteValue { get; set; }

		double Value { get; }

		string[] Tags { get; set; }

		DateTime Date { get; set; }

		string Description { get; set;}

	}
}

