using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PostfixCalculator
{
	//Прочита входния поток и създава обект LexemeSequence
    class Scanner
    {
		private LexemеSequence lexSeq = new LexemеSequence();

        public LexemеSequence GetSequence()
        {
            return lexSeq;
        }

		//Методът може да бъде извикван многократно, като добавя винаги в
		//един и същ обект LexemeSequence. Това е удобно при четене на файл ред по ред.
		public LexemеSequence ScanString(String strInput, int line_number)
        {
			int position = 1;
            while (strInput.Length > 0)
            {
				

                Match identifier = Regex.Match(strInput, @"^[_A-Za-z][_A-Za-z0-9]*");
                if (identifier.Success)
                {
                    strInput = strInput.Remove(0, identifier.Value.Length);
					lexSeq.Add(Lexemе.IDENTIFIER, line_number, position, 0, Lexemе.NO_VALUE);
					position += identifier.Value.Length;
                }
                else
                {
					Match num = Regex.Match(strInput, @"^[0-9]+,?[0-9]*");
                    if (num.Success)
                    {
                        strInput = strInput.Remove(0, num.Value.Length);
						Console.WriteLine(num.Value+"$");
						lexSeq.Add(Lexemе.NUMBER, line_number, position, Lexemе.NO_ADDRESS,
							Double.Parse(num.Value));
						position += num.Value.Length;
                    }

                    else
                    {
                        Match delimiter = Regex.Match(strInput, @"^\s+");
                        if (delimiter.Success)
                        {
                            strInput = strInput.Remove(0, delimiter.Value.Length);
							position += delimiter.Value.Length;
                        }
                        else
                        {
                            Match incr = Regex.Match(strInput, @"^\+\+");
                            if (incr.Success)
                            {
                                strInput = strInput.Remove(0, incr.Value.Length);
								//парсерът ще приема оператора ++ винаги за префиксен; проверката
								//дали е суфиксен ще се прави от TranslationUnit
								lexSeq.Add(Lexemе.PREFIX_INCREMENT, line_number, position, Lexemе.NO_ADDRESS, Lexemе.NO_VALUE);
								position += incr.Value.Length;
                            }
                            else
                            {
                                Match decr = Regex.Match(strInput, @"^\-\-");
                                if (decr.Success)
                                {
                                    strInput = strInput.Remove(0, decr.Value.Length);
									//парсерът ще приема оператора -- винаги за префиксен; проверката
									//дали е суфиксен ще се прави от TranslationUnit
									lexSeq.Add(Lexemе.PREFIX_DECREMENT, line_number, position, Lexemе.NO_ADDRESS, Lexemе.NO_VALUE);
									position += decr.Value.Length;
                                }
                                else
                                {

                                    Match operation = Regex.Match(strInput, @"^[=\|\+\-\*\/%~();&!]");
                                    if (operation.Success)
                                    {
                                        strInput = strInput.Remove(0, operation.Value.Length);
										lexSeq.Add(operation.Value[0], line_number, position, Lexemе.NO_ADDRESS, Lexemе.NO_VALUE);
										position += operation.Value.Length;
                                    }
                                    else
                                    {
										throw new CalculatorException(String.Format(
											"Parsing error, unknown token encountered at line {0}, position {1}.",
											line_number, position));
                                    }
                                }
                            }
                        }
                    }
                }
            }
			return lexSeq;
        }
    }
}
