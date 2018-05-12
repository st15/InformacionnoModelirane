using System;
using System.Collections.Generic;
using System.Text;
using Translator;

namespace TranslationUnit
{
	public class Statement
	{
		private static Dictionary<Char, Expression> expressions;
		private Stack<Lexemе> stack = new Stack<Lexemе>();
		private List<Lexemе> lexemesRPN = new List<Lexemе>();
		//на върха на стека за ОПЗ => ИЗ ще се слага лексема, която
		//ще се означава с долния символ и показва, че това не е
		//истинска лексема, а междинен резултат
		public const char intermediateLexemeSymbol = 'x';

		static Statement()
		{
			loadExpressions();
		}

		private static void loadExpressions()
		{
			expressions = new Dictionary<Char, Expression>();

			addExpression(Lexemе.ASSIGNMENT, -1, 0);
			addExpression(Lexemе.PRINTF, 0, 0);
			addExpression(Lexemе.OPEN_BRACE, -100, 100);
			addExpression(Lexemе.CLOSE_BRACE, Expression.NO_MATTER, Expression.NO_MATTER);
			addExpression(Lexemе.SEMICOLON, Expression.NO_MATTER, Expression.NO_MATTER);
			addExpression(Lexemе.BITWISE_AND, 2, 2);
			addExpression(Lexemе.BITWISE_OR, 2, 2);
			addExpression(Lexemе.PLUS, 3, 3);
			addExpression(Lexemе.MINUS, 3, 3);
			addExpression(Lexemе.MULTUPLICATION, 4, 4);
			addExpression(Lexemе.DIVISION, 4, 4);
			addExpression(Lexemе.MODUL, 4, 4);
			addExpression(Lexemе.UNARY_MINUS, 5, 5);
			addExpression(Lexemе.UNARY_PLUS, 5, 5);
			addExpression(Lexemе.PREFIX_INCREMENT, 5, 5);
			addExpression(Lexemе.PREFIX_DECREMENT, 5, 5);
			addExpression(Lexemе.POSTFIX_INCREMENT, 5, 5);
			addExpression(Lexemе.POSTFIX_DECREMENT, 5, 5);
			addExpression(Lexemе.BITWISE_INVERSION, 5, 5);
			addExpression(Lexemе.BOOLEAN_INVERSION, 5, 5);
		}

		private static void addExpression(char lexemeId, int inStackPriority, int outOfStackPriority)
		{
			expressions.Add(lexemeId, new Expression(lexemeId, inStackPriority, outOfStackPriority));
		}

		public void convertIntoRPN(Lexemе lexeme, Statement postfixOperations)
		{
			Expression expression_sign = null;
			//ако поредния символ е операнд или константа го пишем направо на изходната редица
			if (expressions.TryGetValue(lexeme.id, out expression_sign) == false)
				lexemesRPN.Add(lexeme);
			else
			{
				//това е оператор		
				if (expression_sign.id == Lexemе.CLOSE_BRACE)
				{
					//проверка дали има други оператори в стека
					if (stack.Count > 0)
					{
						//затварящата скоба предизвиква изхвърляне на символи от стека
						//докато се срещне отваряща скоба. Двете се унищожават.
						//взима се лексемата от върха на стека
						Lexemе TOS_lexeme = stack.Peek();
						//стекът не е празен
						Expression TOS_expression_sign = expressions[TOS_lexeme.id];
						while (TOS_expression_sign.id != Lexemе.OPEN_BRACE)
						{
							//предизвиква се изхвърляне на символи от стека към изходната редица
							TOS_lexeme = stack.Pop();
							lexemesRPN.Add(TOS_lexeme);
							//поглежда се какъв е следващия символ от стека
							if (stack.Count > 0)
							{
								TOS_lexeme = stack.Peek();
								TOS_expression_sign = expressions[TOS_lexeme.id];
							}
							else
							{
								//стекът вече е празен
								throw new TranslatorException(
									"Error 1: Closing brace encountered with no corresponding open brace while converting to RPN.");
							}
						}
						//премахва се отварящата скоба от стека
						stack.Pop();
					}
					else
					{
						throw new TranslatorException(
							"Error 2: Closing brace encountered with no corresponding open brace while converting to RPN.");
					}
				}
				else
				{
					//постфиксните ++ и -- се обработват по различен начин
					if ((expression_sign.id == Lexemе.POSTFIX_INCREMENT) ||
						(expression_sign.id == Lexemе.POSTFIX_DECREMENT))
					{
						//добавя се предишния идентификатор
						postfixOperations.lexemesRPN.Add(lexemesRPN[lexemesRPN.Count - 1]);
						//добавя се и самата лексема
						postfixOperations.lexemesRPN.Add(lexeme);
					}
					else
					{
						//проверка за празен стек
						if (stack.Count > 0)
						{
							//обработват се останалите лексеми
							//взима се лексемата от върха на стека
							Lexemе TOS_lexeme = stack.Peek();
							//стекът не е празен
							Expression TOS_expression_sign = expressions[TOS_lexeme.id];
							while (expression_sign.outOfStackPriority <= TOS_expression_sign.inStackPriority)
							{
								//текущият символ е с по-нисък или равен приоритет и ще
								//предизвика изхвърляне на символи от стека към изходната редица
								TOS_lexeme = stack.Pop();
								lexemesRPN.Add(TOS_lexeme);
								//поглежда се какъв е следващия символ от стека
								if (stack.Count > 0)
								{
									TOS_lexeme = stack.Peek();
									TOS_expression_sign = expressions[TOS_lexeme.id];
								}
								else
									break;	//стекът вече е празен
							}
						}
						stack.Push(lexeme);
					}
				}
				if (expression_sign.id == Lexemе.ASSIGNMENT)
					assignmentOperatorDeal();
			}
		}

		private void assignmentOperatorDeal()
		{
			//претърсват се символите от изходната редица и ако има различни
			//от идентификатор или знак '=' значи има семантична грешка
			//По този начин е възможен следния израз:
			//A = B = (C) = scanf();
			//но не и например:
			//A = B + 2 = (C) = scanf();
			IEnumerator<Lexemе> iter = lexemesRPN.GetEnumerator();
			while (iter.MoveNext())
			{
				Lexemе lexeme = iter.Current;
				if (lexeme.id != Lexemе.ASSIGNMENT &&
					lexeme.id != Lexemе.IDENTIFIER &&
					lexeme.id != Lexemе.OPEN_BRACE &&
					lexeme.id != Lexemе.CLOSE_BRACE)
				{
					throw new TranslatorException("Left of assigment sign '=' can be only identifier.");
				}
			}
		}

		//Прехвърля всичко от стека на изходната редица.
		//Връща списъка с постфиксни оператори и операции.
		public void completeRPN()
		{
			while (stack.Count > 0)
				lexemesRPN.Add(stack.Pop());
		}

		//генерира асмеблерен код за целия израз (statement)
		public void generateAssembler(StringBuilder asmCode)
		{
			//изчиствам стека, защото ще го ползвам за генериране на асемблерния код
			stack.Clear();
			IEnumerator<Lexemе> iter = lexemesRPN.GetEnumerator();
			while (iter.MoveNext())
			{
				Lexemе lexeme = iter.Current;
				//ако поредния символ е операнд го пишем в стека
				if (!expressions.ContainsKey(lexeme.id))
					stack.Push(lexeme);

				//това е оператор, може да е унарен или да работи с две стойности
				Assembler.Result result = Assembler.processSingleOperation(lexeme, stack);
				if (result != null)
				{
					asmCode.Append(result.code);
					if (result.savedIn == Assembler.Result.IN_SAVE_PLACE)
					{
						//индикация, че на тази позиция има нещо,
						//чиято стойност е вече сметната
						//и трябва да се вземе например от програмния стек
						stack.Push(new Lexemе(intermediateLexemeSymbol, 0, 0,
							Assembler.getLastAddress(), Lexemе.NO_VALUE));
					}
				}
			}
			//TODO: провери дали стека е празен, защото ако не е празен и
			//израза не е бил само с постфиксни оператори значи има грешка
			//Console.WriteLine("                                                              stack size: " + stack.Count);
			Console.WriteLine("CODE FOR STATEMENT: " + this.ToString() + "\n" + asmCode + "\n");
		}

		public override String ToString()
		{
			StringBuilder content = new StringBuilder("Statement object: ");
			IEnumerator<Lexemе> iter = lexemesRPN.GetEnumerator();
			while (iter.MoveNext())
				content.Append(iter.Current.ToString());
			return content.ToString();
		}
	}
}
