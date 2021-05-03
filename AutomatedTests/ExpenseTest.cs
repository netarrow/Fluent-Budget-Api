using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using System.Collections;
using BudgetUp.Model;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Tests
{
	[TestClass]
	public class ExpenseTest
	{
		[TestMethod]
		public void Expense_MustContain_ValueAndTagsAndDate() {
			var today = DateTime.Today;
			Expense ex = new Expense (5, today, "mangiare", "mensa");

			Assert.AreEqual (5, ex.AbsoluteValue);
			Assert.AreEqual (-5, ex.Value);
			Assert.AreEqual (today, ex.Date);
			CollectionAssert.Contains (ex.Tags, "mangiare");
			CollectionAssert.Contains (ex.Tags, "mensa");
		}

		[TestMethod]
		public void Expense_WhenAddNegativeValue_MustThrowException() {
			Assert.Throws<Exception> (() => new Expense(-5, DateTime.Today, ""));
		}

		[TestMethod]
		public void Expense_TestEquality() {
			Expense ex = new Expense (1, DateTime.Today, "test");
			Expense ex2 = new Expense (1, DateTime.Today, "test");

			Assert.IsTrue (ex.Equals(ex2));
		}

		[TestMethod]
		public void ExpenseIncome_TestEquality() {
			Income income = new Income (1, DateTime.Today, "test");
			Expense ex = new Expense (1, DateTime.Today, "test");

			Assert.IsFalse (ex.Equals(income));
		}
}
}

