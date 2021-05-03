using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BudgetUp.Model
{
	public class Account
	{
		public IEnumerable<IMovement> GetMovementByTag (string tag)
		{
			return movements.Where (e => e.Tags.Contains(tag, StringComparer.InvariantCultureIgnoreCase));
		}

		public IEnumerable<IMovement> GetMovementsByDateRange (DateTime from, DateTime to)
		{
			return movements.Where (item => item.Date >= from && item.Date <= to);
		}

		public void AddMovements (IEnumerable<IMovement> list)
		{
			movements.AddRange (list);
		}

		public double GetBalance ()
		{
			return movements.Sum (item => item.Value);
		}

		public double GetBalanceInPeriod (DateTime from, DateTime to)
		{
			var movementsInRange = GetMovementsInPeriod (from, to);

			return movementsInRange.Sum (item => item.Value);
		}

		IEnumerable<IMovement> GetMovementsInPeriod (DateTime from, DateTime to)
		{
			return movements.Where (item => item.Date >= from && item.Date <= to);
		}

		public List<PartialBalance> GetBalances (ForEach period)
		{
			List<PartialBalance> partialBalance = new List<PartialBalance> ();

			foreach (var item in period.GetPeriods()) {

				partialBalance.Add (new PartialBalance (GetBalanceInPeriod (item.From, item.To), 
				                                        item, 
				                                        GetMovementsInPeriod(item.From, item.To)));

			}

			return partialBalance;
		}

		private List<IMovement> movements;

		public Account ()
		{
			movements = new List<IMovement> ();
		}

		public void AddExpense (Expense ex)
		{
			movements.Add (ex);
		}

		public void AddIncome (Income income)
		{
			movements.Add (income);
		}

		public void ImportFromCsv (ICsvProvider provider)
		{
			ImportFromCsv (provider.GetCsv ());
		}

		public void ImportFromCsv (string csv)
		{
			StringReader reader = new StringReader (csv);

			string line = reader.ReadLine ();

			while (line != null) {
				IMovement movement = Movement.Parse (line);

				if(movement != null)
					movements.Add (movement);

				line = reader.ReadLine ();
			}

		}

		public short Year {
			get;
			set;
		}

		public short Day {
			get;
			set;
		}

		public IEnumerable<IMovement> Movements {
			get {
				return movements;
			}
		}
	}
}

