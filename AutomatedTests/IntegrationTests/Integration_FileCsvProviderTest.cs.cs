using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BudgetUp.Model;
using System.IO;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
	[TestClass]
	public class Integration_FileCsvProviderTest
	{
		[TestMethod]
		public void Integration_GetCsv_MustRead_FromFile() {

			var file = File.CreateText ("File.csv");
			file.WriteLine ("FileContent1");
			file.Write ("FileContent2");
			file.Close ();

			FileCsvProvider fcp = new FileCsvProvider ("File.csv");

			string content = fcp.GetCsv ();

			Assert.AreEqual (string.Format("FileContent1{0}FileContent2", Environment.NewLine), content);

			File.Delete ("File.csv");

		}

	}
} 

