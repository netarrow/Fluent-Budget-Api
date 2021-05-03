using System;
using BudgetUp.Model;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Dates;


namespace BudgetUp.ConsoleUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Account account = new Account ();
			account.ImportFromCsv (new FileCsvProvider ("movimenti.csv"));

			Console.WriteLine ("Partial balances month by month starting from income day");

			List<PartialBalance> partialBalances = account.GetBalances (ForEach.Month.StartingFrom(The.Year(2012).On.January.The10th));

			foreach (var balance in partialBalances) {
				Console.Write (balance.Period.ToString());
				Console.Write ("********");
				Console.WriteLine (balance.Balance);
				foreach (var movement in balance.Movements) {
					Console.WriteLine (string.Format("\t{0}\t{1}\t{2}\t\t\t\t\t{3}", movement.Date, movement.Value, string.Join(",", movement.Tags), movement.Description));
				}
			}

			Console.WriteLine ();
			Console.WriteLine ();

			Console.WriteLine ("Partial balances day by day");

			List<PartialBalance> balancesDay = account.GetBalances (ForEach.Day.StartingFrom(The.Year(2012).On.January.The1st));

			foreach (var balance in balancesDay) {
				Console.Write (balance.Period.ToString());
				Console.Write ("********");
				Console.WriteLine (balance.Balance);
				foreach (var movement in balance.Movements) {
					Console.WriteLine (string.Format("\t{0}\t{1}\t{2}\t\t\t\t\t{3}", movement.Date, movement.Value, string.Join(",", movement.Tags), movement.Description));
				}
			}

		    Console.ReadKey();

		}
	}
}
