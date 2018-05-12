using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostfixCalculator
{
	class CalculatorException : Exception
	{
		public CalculatorException(string message)
			: base(message)
		{
	    }
	}
}
