using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BudgetUp.Model;
using System.IO;
using Moq;
using Moq.Protected;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
	[TestClass]
	public class FileCsvProviderTest
	{
		[TestMethod]
		public void GetCsv_MustReturn_TheFileContent_AsString() {

			var mock = new Mock<FileCsvProvider> ("FileName.csv");

			mock.Protected ()
				.Setup<string> ("ReadFromFile")
				.Returns ("FileContent");

			string content = mock.Object.GetCsv ();

			Assert.AreEqual ("FileContent", content);

		}
	}
}

