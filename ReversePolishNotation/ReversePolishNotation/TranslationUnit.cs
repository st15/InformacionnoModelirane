using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PostfixCalculator;

namespace TranslationUnit
{
	public class TranslationUnit
	{
		private double result;

		//Обработва Statement по Statement
		public bool parseStatements(LexemеSequence input)
		{
			//входната поредица ще се обработва израз по израз (разделени са с ';')
			Lexemе lexemе = null;
			try
			{
				input.Rewind();
				int input_size = input.Size();
				Statement stmt = new Statement();
				//съдържа идентификаторите с постфиксни ++ и --
				for (int i = 0; i < input_size; i++)
				{
					lexemе = input.GetLexemе();
					if (lexemе.id != Lexemе.SEMICOLON)
					{
						checkUnaryMinus(input, lexemе);
						checkUnaryPlus(input, lexemе);
						stmt.convertIntoRPN(lexemе);
					}
					else
					{
						//вече е създаден ОПЗ, ще смятам резултата
						//прехвърля всичко от стека на изходната редица
						stmt.completeRPN();
						//смятам резултата
						result = stmt.Calculate();
						stmt = new Statement();
					}
				}
			}
			catch (CalculatorException e)
			{
				if (lexemе != null)
					Console.WriteLine(String.Format("Error at line {0}, position {1}: {2}",
						lexemе.row, lexemе.positionAtRow, e.Message));
				if (Startup.DEBUG)
					Console.WriteLine(e.StackTrace);
				return false;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}
			return true;
		}

		public double getResult()
		{
			return this.result;
		}

		//Скенерът приема всеки знак '-' за аритметична операция, затова тук се проверява
		//дали всъщност знакът е бил унарен оператор.
		//Замяната на знака се налага заради операциите със стека на ОПЗ, да се знае дали
		//да се прилага операцията върху един или два операнда.
		private void checkUnaryMinus(LexemеSequence input, Lexemе lexeme)
		{
			if (lexeme.id == Lexemе.MINUS)
			{
				bool unary_sign = false;
				if (input.GetPosition() > 1)
				{
					char symbol = input.GetLexemе(input.GetPosition() - 2).id;
					if ((symbol == Lexemе.ASSIGNMENT) ||
						(symbol == Lexemе.OPEN_BRACE) ||
						(symbol == Lexemе.SEMICOLON) ||
						(symbol == Lexemе.BITWISE_AND) ||
						(symbol == Lexemе.BITWISE_OR) ||
						(symbol == Lexemе.PLUS) ||
						(symbol == Lexemе.MINUS) ||
						(symbol == Lexemе.MULTUPLICATION) ||
						(symbol == Lexemе.DIVISION) ||
						(symbol == Lexemе.MODUL) ||
						(symbol == Lexemе.BITWISE_INVERSION) ||
						(symbol == Lexemе.BOOLEAN_INVERSION))
						unary_sign = true;
				}
				else
					unary_sign = true;
				if (unary_sign)
					lexeme.id = Lexemе.UNARY_MINUS;
			}
		}

		//Скенерът приема всеки знак '+' за аритметична операция, затова тук се проверява
		//дали всъщност знакът е бил унарен оператор.
		//Замяната на знака се налага заради операциите със стека на ОПЗ, да се знае дали
		//да се прилага операцията върху един или два операнда.
		private void checkUnaryPlus(LexemеSequence input, Lexemе lexeme)
		{
			if (lexeme.id == Lexemе.PLUS)
			{
				bool unary_sign = false;
				if (input.GetPosition() > 1)
				{
					char symbol = input.GetLexemе(input.GetPosition() - 2).id;
					if (
						(symbol == Lexemе.ASSIGNMENT) ||
						(symbol == Lexemе.OPEN_BRACE) ||
						(symbol == Lexemе.SEMICOLON) ||
						(symbol == Lexemе.BITWISE_AND) ||
						(symbol == Lexemе.BITWISE_OR) ||
						(symbol == Lexemе.PLUS) ||
						(symbol == Lexemе.MINUS) ||
						(symbol == Lexemе.MULTUPLICATION) ||
						(symbol == Lexemе.DIVISION) ||
						(symbol == Lexemе.MODUL) ||
						(symbol == Lexemе.BITWISE_INVERSION) ||
						(symbol == Lexemе.BOOLEAN_INVERSION))
						unary_sign = true;
				}
				else
					unary_sign = true;
				if (unary_sign)
					lexeme.id = Lexemе.UNARY_PLUS;
			}
		}
	}
}
