using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TranslationUnit;

namespace PostfixCalculator
{
    public class Startup
    {
        public static bool DEBUG = false;

        static void Main(string[] args)
        {
            Scanner scanner = new Scanner();

            LexemеSequence input = null;
            try
            {
                input = scanner.ScanString("a=(5,78-6)*6/8", 0);
                //слагам знак ';' накрая, ако вече няма такъв
                if (input.GetLexemе(input.Size() - 1).id != Lexemе.SEMICOLON)
                    input.Add(Lexemе.SEMICOLON);
            }
            catch (CalculatorException e)
            {
                Console.WriteLine(e.Message);
                if (Startup.DEBUG)
                    Console.WriteLine(e.StackTrace);
                Console.WriteLine("Input file parsing failed!");
                keepConsoleWindowOpen();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Input file parsing failed!");
                keepConsoleWindowOpen();
                return;
            }

            TranslationUnit.TranslationUnit tr_unit = new TranslationUnit.TranslationUnit();

            if (tr_unit.parseStatements(input))
            {
                Console.WriteLine(tr_unit.getResult());
                Console.WriteLine("Translation succeed!");
            }
            else
                Console.WriteLine("Translation failed!");

            keepConsoleWindowOpen();
        }

        public static Double SolveRPN(String expression)
        {
			Scanner scanner = new Scanner();
			
			LexemеSequence input = null;
			try
			{
                input = scanner.ScanString(expression, 0);
				//слагам знак ';' накрая, ако вече няма такъв (това ми е условието за край на израз)
				if (input.GetLexemе(input.Size() - 1).id != Lexemе.SEMICOLON)
					input.Add(Lexemе.SEMICOLON);
			}
			catch (CalculatorException e)
			{
				Console.WriteLine(e.Message);
				if (Startup.DEBUG)
					Console.WriteLine(e.StackTrace);
				Console.WriteLine("Input parsing failed!");
                return Double.NaN;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine("Input parsing failed!");
                return Double.NaN;
			}
	        
			TranslationUnit.TranslationUnit tr_unit = new TranslationUnit.TranslationUnit();

            if (tr_unit.parseStatements(input))
            {
                Console.WriteLine(tr_unit.getResult());
                return tr_unit.getResult();
            }
            else
            {
                Console.WriteLine("Translation failed!");
                return Double.NaN;
            }
        }

		private static void keepConsoleWindowOpen()
		{
			Console.WriteLine("\n\n\nPress any key to continue...");
			Console.ReadKey();
		}
    }
}
