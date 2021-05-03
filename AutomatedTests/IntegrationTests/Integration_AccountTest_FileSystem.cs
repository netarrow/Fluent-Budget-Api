using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BudgetUp.Model;
using System.Linq;
using System.IO;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
	[TestClass]
	public class Integration_AccountTest_FileSystem
	{
		[TestMethod]
		public void ImportFromCsv_MustImport_FromFile ()
		{
			var file = File.CreateText ("File.csv");
			file.WriteLine ("Banca,01 Ago 12,mangiare uscite,happy hour sannabar,\"-4 €");
			file.Write ("Banca,2/1/2012,ricarica-cellulare,,-15 €");
			file.Close ();

			Account account = new Account ();

			account.ImportFromCsv (new FileCsvProvider("File.csv"));

			Assert.AreEqual (2, account.Movements.Count());
			Assert.AreEqual (-4, account.Movements.ElementAt(0).Value);
			Assert.AreEqual (-15, account.Movements.ElementAt(1).Value);

			File.Delete ("File.csv");

		}

		[TestMethod]
		public void ImportFromCsv_BigFile_MustImport_FromFile () {

			Account account = new Account ();

			account.ImportFromCsv (new FileCsvProvider("IntegrationTests/movimenti.csv"));

			Assert.AreEqual (11, account.Movements.Count ());

		}

	}
}

