using System;
using System.Collections.Generic;
using System.Linq;
using BudgetUp.Model;

namespace BudgetUp.Model
{
	public interface ICsvProvider
	{
		string GetCsv();
	}
}

