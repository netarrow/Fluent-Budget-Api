using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using FizzWare.NBuilder.Dates;
using System.Collections.Generic;
using BudgetUp.Model;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
	[TestClass]
	public class ForEachTest
	{
		[TestMethod]
		public void ForEach_Create_Monthly_Periods ()
		{
			ForEach fe = ForEach.Month;

			List<Period> periods = fe.GetPeriods ();

			Assert.AreEqual (12, periods.Count);

			Assert.AreEqual (1, periods[0].From.Day);
			Assert.AreEqual (31, periods[0].To.Day);

		}

		[TestMethod]
		public void ForEach_Create_Monthly_Periods_StartingFrom_10thOfMont ()
		{
			ForEach fe = ForEach.Month.StartingFrom (The.Year (2012).On.January.The10th);

			List<Period> periods = fe.GetPeriods ();

			Assert.AreEqual (12, periods.Count);
			Assert.AreEqual (1, periods[0].From.Month);
			Assert.AreEqual (2, periods[0].To.Month);

			Assert.AreEqual (10, periods[0].From.Day);
			Assert.AreEqual (9, periods[0].To.Day);

			Assert.AreEqual (10, periods[1].From.Day);
			Assert.AreEqual (9, periods[1].To.Day);

		}

		[TestMethod]
		public void ForEach_Create_Daily_Recurrency() {
			ForEach fe = ForEach.Day.StartingFrom(The.Year(2012).On.January.The1st);

			List<Period> periods = fe.GetPeriods ();

			Assert.AreEqual (1, periods[0].From.Day);
			Assert.AreEqual (1, periods[0].From.Month);
			Assert.AreEqual (2012, periods[0].From.Year);

			Assert.AreEqual (2, periods[1].From.Day);
			Assert.AreEqual (1, periods[1].From.Month);
			Assert.AreEqual (2012, periods[1].From.Year);

			Assert.AreEqual (31, periods.Last().From.Day);
			Assert.AreEqual (12, periods.Last().From.Month);
			Assert.AreEqual (2012, periods.Last().From.Year);
	}
}
}