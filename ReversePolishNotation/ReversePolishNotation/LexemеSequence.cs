using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PostfixCalculator
{
    public class LexemеSequence
    {
		private List<Lexemе> sequence = new List<Lexemе>();
        private int position;
        
        // Връща идентификатора на лексемата
        public char Get()
        {
	        char id = Peek();
            position++;
            return id;
        }
        
        public Lexemе GetLexemе()
        {
	        Lexemе lexeme = PeekLexemе();
            position++;
            return lexeme;
        }
        
        public Lexemе GetLexemе(int pos)
        {
            return sequence[pos];
        }
        
        public Lexemе PeekLexemе()
        {
	        if(position >= sequence.Count)
	            throw new CalculatorException("InputSequence out of bounds.");
	        return sequence[position];
        }

		// Връща идентификатора на лексемата
        public char Peek()
        {
	        if(position >= sequence.Count)
	            throw new CalculatorException("InputSequence out of bounds.");
	        return (sequence[position]).id;
        }
        
        public void Add(char symbol)
        {
	        sequence.Add(new Lexemе(symbol, 0, 0, Lexemе.NO_ADDRESS, Lexemе.NO_VALUE));
        }

		public void Add(char symbol, double value)
		{
			sequence.Add(new Lexemе(symbol, 0, 0, Lexemе.NO_ADDRESS, value));
		}

        public void Add(char symbol, int line, int positionAtLine, int address, double value)
        {
            sequence.Add(new Lexemе(symbol, line, positionAtLine, address, value));
        }
        
        public int GetPosition()
        {
	        return position;
        }
        
        
        public int Size()
        {
	        return sequence.Count;
        }
        
        public void Rewind()
        {
			position = 0;
        }

		//връща последователността от лексеми от текущата позиция до края
		public override String ToString()
		{
			StringBuilder symbol_sequence = new StringBuilder(sequence.Count);
			int pos = 0;
			IEnumerator<Lexemе> iter = sequence.GetEnumerator();
			while (iter.MoveNext())
			{
				Lexemе lexeme = iter.Current;
				if (pos >= position)
					symbol_sequence.Append(lexeme.id);
				pos++;
			}
			return symbol_sequence.ToString();
		}
    }
}
