using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using System.Linq;
using BudgetUp.Model;

namespace BudgetUp.Model
{
	public class PartialBalance
	{
		public PartialBalance (double balance, Period period, IEnumerable<IMovement> Movements)
		{
			this.Balance = balance;
			this.Period = period;
			this.Movements = Movements;
		}

		IEnumerable<IMovement> movements;
		public IEnumerable<IMovement> Movements {
			get {
				return movements.OrderBy(item => item.Date);
			}
			private set {
				movements = value;
			}
		}

		public double Balance { get; private set;}

		public Period Period { get; private set;}


	}
}

