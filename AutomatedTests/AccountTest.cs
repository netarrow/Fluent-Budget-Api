using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using System.Linq;
using BudgetUp.Model;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Tests
{
	[TestClass]
	public class AccountTest
	{
		[TestMethod]
		public void Add_ExpenseOnAccount_MustBeAdded ()
		{

			Account account = new Account ();
			Expense ex = new Expense (5, DateTime.Today);

			account.AddExpense (ex);
		
			CollectionAssert.Contains (account.Movements, ex);

		}

		[TestMethod]
		public void Add_IncomeOnAccount_MustBeAdded ()
		{
			Account account = new Account ();
			Income income = new Income (100, DateTime.Today);

			account.AddIncome (income);

			CollectionAssert.Contains (account.Movements, income);
		}

		[TestMethod]
		public void GetBalance_MustReturn_TheBalanceBetweenIncomeAndExpense ()
		{
			Account account = new Account ();
			Expense ex = new Expense (100, DateTime.Today);
			Expense ex2 = new Expense (50, DateTime.Today);
			Income income = new Income (200, DateTime.Today);

			account.AddExpense (ex);
			account.AddExpense (ex2);
			account.AddIncome (income);

			double balance = account.GetBalance ();

			Assert.AreEqual (50, balance);
		}

		[TestMethod]
		public void GetMovementsByTag_MustReturns_TheTaggedMovements ()
		{
			Account account = new Account ();

			Expense ex = new Expense (100, DateTime.Today, "mangiare");
			Expense ex2 = new Expense (10, DateTime.Today, "cellulare");
			Income income = new Income (1000, DateTime.Today, "stipendio");
			Income income2 = new Income (25, DateTime.Today, "Cellulare");

			account.AddExpense (ex);
			account.AddExpense (ex2);
			account.AddIncome (income);
			account.AddIncome (income2);

			IEnumerable<IMovement> movements = account.GetMovementByTag ("Cellulare");

			CollectionAssert.DoesNotContain (movements, ex);
			CollectionAssert.DoesNotContain (movements, income);
			CollectionAssert.Contains (movements, ex2);
			CollectionAssert.Contains (movements, income2);
		}

		[TestMethod]
		public void GetMovementsByDateRange_MustReturns_MovementsInRange ()
		{
			Account account = new Account ();

			Expense ex1 = new Expense (1, new DateTime (2013, 1, 1)); // skip
			Expense ex2 = new Expense (1, new DateTime (2013, 1, 10)); // keep
			Expense ex3 = new Expense (1, new DateTime (2013, 1, 15)); // keep
			Expense ex4 = new Expense (1, new DateTime (2013, 2, 1)); // keep
			Expense ex5 = new Expense (1, new DateTime (2013, 2, 10)); // keep
			Expense ex6 = new Expense (1, new DateTime (2013, 2, 12)); // skip

			var from = new DateTime (2013, 1, 10);
			var to = new DateTime (2013, 2, 10);

			account.AddMovements (new List<IMovement>() {ex1, ex2, ex3, ex4, ex5, ex6});

			IEnumerable<IMovement> movements = account.GetMovementsByDateRange (from, to);

			CollectionAssert.Contains (movements, ex2);
			CollectionAssert.Contains (movements, ex3);
			CollectionAssert.Contains (movements, ex4);
			CollectionAssert.Contains (movements, ex5);
			CollectionAssert.DoesNotContain (movements, ex1);
			CollectionAssert.DoesNotContain (movements, ex6);
		}

		[TestMethod]
		public void AddMovements_Movements_MustBeAdded ()
		{
			Account account = new Account ();

			RandomGenerator generator = new RandomGenerator ();

			var list = Builder<Expense>.CreateListOfSize (45).All ().WithConstructor (() => new Expense (1, generator.Next (January.The1st, February.The15th))).Build ();

			account.AddMovements (list);

			CollectionAssert.AreEquivalent (list, account.Movements);

		}

		[TestMethod]
		public void ImportFromCsv_MustAdd_TheMovement() {
			Account account = new Account ();

			account.ImportFromCsv ("Banca,2/1/2012,ricarica-cellulare,,-15 €");

			Assert.AreEqual(The.Year(2012).On.February.The1st, account.Movements.ElementAt (0).Date);
			Assert.AreEqual(-15, account.Movements.ElementAt (0).Value);
			Assert.IsTrue(account.Movements.ElementAt (0).Tags.Contains("ricarica-cellulare"));

			account.ImportFromCsv ("Banca,01 Ago 12,mangiare uscite,happy hour sannabar,\"-4 €\"");

			Assert.AreEqual(The.Year(2012).On.August.The1st, account.Movements.ElementAt (1).Date);
			Assert.AreEqual(-4, account.Movements.ElementAt (1).Value);
			Assert.IsTrue(account.Movements.ElementAt (1).Tags.Contains("mangiare"));
			Assert.IsTrue(account.Movements.ElementAt (1).Tags.Contains("uscite"));
		}

		[TestMethod]
		public void ImportFromCsv_MustIgnore_NotBankMovement() {
			Account account = new Account ();

			account.ImportFromCsv ("Carta di Credito,2/4/2012,software,metal gear HD collection,-33 €");

			Assert.IsFalse (account.Movements.Any());
		}

		[TestMethod]
		public void ImportFromCsv_MultiLineCsv_MustImport_TheMovements() {

			string csvContent = "Banca,01 Ago 12,mangiare uscite,happy hour sannabar,\"-4 €\"\n\r" +
								"Banca,2/1/2012,ricarica-cellulare,,-15 €";

			var mock = new Mock<ICsvProvider>();

			mock.Setup (csv => csv.GetCsv ()).Returns (csvContent);

			Account account = new Account ();

			account.ImportFromCsv(mock.Object);

			Assert.AreEqual (2, account.Movements.Count());
			Assert.AreEqual (-4, account.Movements.ElementAt(0).Value);
			Assert.AreEqual (-15, account.Movements.ElementAt(1).Value);

		}

		[TestMethod]
		public void GetBalanceInPeriod_MustReturn_BalanceOfperiod() {

			Account account = new Account ();

			// movements in range
			account.AddIncome (new Income (1200, The.Year (2012).On.January.The10th));
			account.AddExpense (new Expense (5, The.Year (2012).On.January.The15th));
			account.AddExpense (new Expense (400, The.Year (2012).On.January.The30th));
			account.AddExpense (new Expense (120, The.Year (2012).On.February.The4th));
			account.AddIncome (new Income (100, The.Year (2012).On.February.The6th));

			// some movement extra range. Must be skipped
			account.AddExpense (new Expense (120, The.Year (2012).On.April.The4th));
			account.AddIncome (new Income (100, The.Year (2012).On.April.The6th));

			var from = The.Year (2012).On.January.The10th;
			var to = The.Year (2012).On.February.The9th; 

			double balance = account.GetBalanceInPeriod (from, to);

			Assert.AreEqual (1200 - 5 - 400 - 120 + 100, balance);

		}

		[TestMethod]
		public void GetBalances_ForEachMonth_StartingFrom_MustReturns_Balances() {

			Account account = new Account ();

			account.AddIncome (new Income(100, The.Year(2012).On.January.The1st));
			account.AddExpense (new Expense(80, The.Year(2012).On.January.The1st));

			account.AddIncome (new Income(1000, The.Year(2012).On.April.The1st));
			account.AddExpense (new Expense(600, The.Year(2012).On.August.The1st));

			List<PartialBalance> balances = account.GetBalances(ForEach.Month.StartingFrom(The.Year(2012).On.January.The1st));

			Assert.AreEqual (12, balances.Count());
			Assert.AreEqual (20, balances.ElementAt(0).Balance);

			Assert.AreEqual (1000, balances.ElementAt(3).Balance);
			Assert.AreEqual (-600, balances.ElementAt(7).Balance);

		}

		[TestMethod]
		public void GetBalances_ForEachDay_StartingFrom_MustReturns_Balances() {

			Account account = new Account ();

			account.AddIncome (new Income(100, The.Year(2012).On.January.The1st));
			account.AddExpense (new Expense(80, The.Year(2012).On.January.The1st));

			List<PartialBalance> balances = account.GetBalances(ForEach.Day.StartingFrom(The.Year(2012).On.January.The1st));

			Assert.AreEqual (20, balances.ElementAt(0).Balance);

		}

	}
}

