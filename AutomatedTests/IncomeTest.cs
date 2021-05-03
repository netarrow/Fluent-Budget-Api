using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BudgetUp.Model;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Tests
{
	[TestClass]
	public class IncomeTest
	{
		[TestMethod]
		public void Income_MustContain_ValueAndTagsAndDescription() {
			var today = DateTime.Today;
			Income income = new Income ("description", 100, today, "stipendio");

			Assert.AreEqual (100, income.AbsoluteValue);
			Assert.AreEqual (100, income.Value);
			Assert.AreEqual (today, income.Date);
			Assert.AreEqual ("description", income.Description);
			CollectionAssert.Contains (income.Tags, "stipendio");
		}

		[TestMethod]
		public void Income_WhenAddNegativeValue_MustThrowException() {
			Assert.Throws<Exception> (() => new Income(-100, DateTime.Today));
		}

		[TestMethod]
		public void Income_TestEquality() {
			Income income = new Income (1, DateTime.Today, "test");
			Income income2 = new Income (1, DateTime.Today, "test");

			Assert.IsTrue (income.Equals(income2));
		}

		[TestMethod]
		public void IncomeExpense_TestEquality() {
			Income income = new Income (1, DateTime.Today, "test");
			Expense ex = new Expense (1, DateTime.Today, "test");

			Assert.IsFalse (income.Equals(ex));
		}
	}
}

