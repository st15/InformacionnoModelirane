using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationUnit
{
    public class Expression
    {
        public readonly char id;
		public readonly int inStackPriority;
		public readonly int outOfStackPriority;

        public static readonly int NO_MATTER = -1000000;

		public Expression(char symbol, int inStackPriority, int outOfStackPriority)
        {
			this.id = symbol;
			this.inStackPriority = inStackPriority;
			this.outOfStackPriority = outOfStackPriority;
        }
    }
}
