using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BudgetUp.Model;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
	[TestClass]
	public class MovementTest
	{
		[TestMethod]
		public void Equals_WhenEquals_MustReturn_True()
		{
			var now = DateTime.Now;
			Movement a = new Expense (1, now, "libri");
			Movement b = new Expense (1, now, "libri");

			Movement c = new Income (2, now, "libri");
			Movement d = new Income (2, now, "libri");

			Assert.IsTrue (a.Equals (b));
			Assert.IsTrue (c.Equals (d));
		}

		[TestMethod]
		public void Equals_WhenDifferent_MustReturn_False()
		{
			var now = DateTime.Now;
			Movement a = new Expense (1, now, "libri");
			Movement b = new Expense (2, now, "libri");

			Movement c = new Income (1, now, "libri");
			Movement d = new Income (2, now, "libri");

			Assert.IsFalse (a.Equals (b));
			Assert.IsFalse (c.Equals (d));

		}

		[TestMethod]
		public void ParseCsv_FromCsv_CreateMovement_Expense() {

			var line = "Banca,2/1/2012,ricarica-cellulare,,-15 €";

			IMovement ex =  Movement.Parse (line);

			Assert.AreEqual (The.Year(2012).On.February.The1st, ex.Date);
			Assert.AreEqual (-15, ex.Value);
		}

		[TestMethod]
		public void ParseCsv_FromCsv_CreateMovement_Income() {

			var line = "Banca,2/1/2012,ricarica-cellulare,,15 €";

			IMovement ex =  Movement.Parse (line);

			Assert.AreEqual (The.Year(2012).On.February.The1st, ex.Date);
			Assert.AreEqual (15, ex.Value);
		}

		[TestMethod]
		public void ParseCsv_FromCsv_DateFormat_Mddyyyy_MustBeParsed() {

			var line = "Banca,1/13/2013,ricarica-cellulare,,15 €";

			IMovement ex =  Movement.Parse (line);

			var expected = The.Year (2013).On.January.The13th;

			Assert.AreEqual (expected, ex.Date);
		}

		[TestMethod]
		public void ParseCsv_FromCsv_DateFormat_ddMMMyy_MustBeParsed() {

			var line = "Banca,17 Gen 12,ricarica-cellulare,,15 €";

			IMovement ex =  Movement.Parse (line);

			var expected = The.Year (2012).On.January.The17th;

			Assert.AreEqual (expected, ex.Date);
		}

	}
}

