using System;
using System.Collections.Generic;
using System.Text;

namespace PostfixCalculator
{
    public class Lexemе
    {
        public char id; //символ, с който се означава типа на лексемата
        public readonly int row; //номер на ред във входния файл
		public readonly int positionAtRow; //номер на позицията на реда във входния файл
		public readonly int address; //само за идентификатори
		public double value; //само за константи
        
        public const int NO_VALUE = -1;
        public const int NO_ADDRESS = -1;

		public const char ASSIGNMENT = '=';
		public const char SCANF = 'a';
		public const char PRINTF = 'p';
		public const char IDENTIFIER = 'n';
		public const char NUMBER = 'b';
		public const char OPEN_BRACE = '(';
		public const char CLOSE_BRACE = ')';
		public const char BITWISE_AND = '&';
		public const char BITWISE_OR = '|';
		public const char BITWISE_INVERSION = '~';
		public const char BOOLEAN_INVERSION = '!';
		public const char PLUS = '+';
		public const char UNARY_PLUS = 'y';
		public const char MINUS = '-';
		public const char UNARY_MINUS = 'z';
		public const char MULTUPLICATION = '*';
		public const char DIVISION = '/';
		public const char MODUL = '%';
		public const char PREFIX_INCREMENT = 'i';
		public const char POSTFIX_INCREMENT = 'w';
		public const char PREFIX_DECREMENT = 'd';
		public const char POSTFIX_DECREMENT = 'v';
		public const char SEMICOLON = ';';

		public Lexemе(char id, double value)
			:this(id, 0, 0, 0, value)
		{
			
		}
        
        public Lexemе(char id, int row, int positionAtRow, int address, double value)
        {
	        this.id = id;
            this.row = row;
			this.positionAtRow = positionAtRow;
            this.address = address;
            this.value = value;
        }
        
        public override String ToString()
        {
            return id.ToString();
        }
    }
}
