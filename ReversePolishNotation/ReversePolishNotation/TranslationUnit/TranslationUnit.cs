using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Translator;

namespace TranslationUnit
{
	public class TranslationUnit
	{
		private StringBuilder asmCode = new StringBuilder();

		//Обработва Statement по Statement и генерира асемблерен код.
		public bool parseStatements(LexemеSequence input)
		{
			//входната поредица ще се обработва израз по израз (разделени са с ';')
			Lexemе lexemе = null;
			try
			{
				asmCode.Append(Assembler.asmHeader);
				input.rewind();
				int input_size = input.size();
				Statement stmt = new Statement();
				//съдържа идентификаторите с постфиксни ++ и --
				Statement postfixOperations = new Statement();
				for (int i = 0; i < input_size; i++)
				{
					lexemе = input.getLexemе();
					if (lexemе.id != Lexemе.SEMICOLON)
					{
						incAndDecOperatorDeal(input, lexemе);
						checkUnaryMinus(input, lexemе);
						checkUnaryPlus(input, lexemе);
						stmt.convertIntoRPN(lexemе, postfixOperations);
					}
					else
					{
						//прехвърля всичко от стека на изходната редица
						stmt.completeRPN();
						//израза се преобразува в асемблерен код
						stmt.generateAssembler(asmCode);
						postfixOperations.generateAssembler(asmCode);
						stmt = new Statement();
						postfixOperations = new Statement();
					}
				}
				asmCode.Append(Assembler.returnDOS);
				asmCode.Append(Assembler.getIncludedProcsCode());
				asmCode.Append(Assembler.asmFooter);
				Console.WriteLine("\nASSEMBLER CODE:\n" + asmCode);
			}
			catch (TranslatorException e)
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

		public String GetAsmCode()
		{
			return this.asmCode.ToString();
		}

		//Парсерът приема всеки знак '-' за аритметична операция, затова тук се проверява
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
					char symbol = input.getLexemе(input.GetPosition() - 2).id;
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

		//Парсерът приема всеки знак '+' за аритметична операция, затова тук се проверява
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
					char symbol = input.getLexemе(input.GetPosition() - 2).id;
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

		//Открива и заменя постфиксни ++ и -- с префиксни при необходимост.
		//Парсерът приема тези оператори винаги за префиксни, затова тук се прави проверката дали
		//всъщност са суфиксни, при което идентификатора на лексемата ще се смени със съответния й.
		private void incAndDecOperatorDeal(LexemеSequence input, Lexemе lexeme)
		{
			changeSymbol(input, lexeme, Lexemе.PREFIX_INCREMENT, Lexemе.POSTFIX_INCREMENT);
			changeSymbol(input, lexeme, Lexemе.PREFIX_DECREMENT, Lexemе.POSTFIX_DECREMENT);
		}

		private void changeSymbol(LexemеSequence input, Lexemе lexeme, char search_symbol,
			char replace_symbol)
		{
			if (lexeme.id == search_symbol)
			{
				//проверка дали предходната лексема е идентификатор
				if (input.GetPosition() > 1)
				{
					if (input.getLexemе(input.GetPosition() - 2).id == Lexemе.IDENTIFIER)
					{
						lexeme.id = replace_symbol;
						return;
					}
				}
				//проверка дали следващата лексема е идентификатор
				if (input.GetPosition() < input.size())
				{
					char symb = input.peekLexemе().id;
					if (input.peekLexemе().id == Lexemе.IDENTIFIER)
						return;
					else
						throw new TranslatorException("Error 1: Expression " + search_symbol +
							" can be applyed only to identifier.");
				}
				throw new TranslatorException("Error 2: Expression " + search_symbol +
							" can be applyed only to identifier.");
			}
		}
	}
}
