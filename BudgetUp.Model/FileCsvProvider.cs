using System;
using System.IO;

namespace BudgetUp.Model
{
	public class FileCsvProvider : ICsvProvider
	{
		string fileName;

		public FileCsvProvider (string fileName)
		{
			this.fileName = fileName;

		}

		#region ICsvProvider implementation

		public string GetCsv ()
		{
			return ReadFromFile ();
		}

		#endregion


		protected virtual string ReadFromFile ()
		{
			using (TextReader reader = File.OpenText (fileName)) {
				return reader.ReadToEnd ();
			}
		}
	}
}

