using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PostfixCalculator
{
	//������� ������� ����� � ������� ����� LexemeSequence
    class Scanner
    {
		private Lexem�Sequence lexSeq = new Lexem�Sequence();

        public Lexem�Sequence GetSequence()
        {
            return lexSeq;
        }

		//������� ���� �� ���� �������� �����������, ���� ������ ������ �
		//���� � ��� ����� LexemeSequence. ���� � ������ ��� ������ �� ���� ��� �� ���.
		public Lexem�Sequence ScanString(String strInput, int line_number)
        {
			int position = 1;
            while (strInput.Length > 0)
            {
				

                Match identifier = Regex.Match(strInput, @"^[_A-Za-z][_A-Za-z0-9]*");
                if (identifier.Success)
                {
                    strInput = strInput.Remove(0, identifier.Value.Length);
					lexSeq.Add(Lexem�.IDENTIFIER, line_number, position, 0, Lexem�.NO_VALUE);
					position += identifier.Value.Length;
                }
                else
                {
					Match num = Regex.Match(strInput, @"^[0-9]+,?[0-9]*");
                    if (num.Success)
                    {
                        strInput = strInput.Remove(0, num.Value.Length);
						Console.WriteLine(num.Value+"$");
						lexSeq.Add(Lexem�.NUMBER, line_number, position, Lexem�.NO_ADDRESS,
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
								//�������� �� ������ ��������� ++ ������ �� ���������; ����������
								//���� � �������� �� �� ����� �� TranslationUnit
								lexSeq.Add(Lexem�.PREFIX_INCREMENT, line_number, position, Lexem�.NO_ADDRESS, Lexem�.NO_VALUE);
								position += incr.Value.Length;
                            }
                            else
                            {
                                Match decr = Regex.Match(strInput, @"^\-\-");
                                if (decr.Success)
                                {
                                    strInput = strInput.Remove(0, decr.Value.Length);
									//�������� �� ������ ��������� -- ������ �� ���������; ����������
									//���� � �������� �� �� ����� �� TranslationUnit
									lexSeq.Add(Lexem�.PREFIX_DECREMENT, line_number, position, Lexem�.NO_ADDRESS, Lexem�.NO_VALUE);
									position += decr.Value.Length;
                                }
                                else
                                {

                                    Match operation = Regex.Match(strInput, @"^[=\|\+\-\*\/%~();&!]");
                                    if (operation.Success)
                                    {
                                        strInput = strInput.Remove(0, operation.Value.Length);
										lexSeq.Add(operation.Value[0], line_number, position, Lexem�.NO_ADDRESS, Lexem�.NO_VALUE);
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
