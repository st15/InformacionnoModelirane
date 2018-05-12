using System;
using System.Collections.Generic;
using System.Text;
using Translator;

namespace TranslationUnit
{
	public abstract class Assembler
	{
		private static String savePlace = "dword ptr [ebp+{0}]";

		private static HashSet<String> includedProcs = new HashSet<String>();
		private static int nextAvailableAddress = 396;

		private static readonly String newLine = "\r\n";
		private static readonly String scanfCode;
		private static readonly String printfCode;
		public static readonly String asmHeader;
		public static readonly String asmFooter;
		public static readonly String returnDOS;

		static Assembler()
		{
			//инициализация на стрингови константи
			asmHeader =
				"STACK_SEG SEGMENT STACK" + newLine +
				"	DW 512 DUP(?)" + newLine +
				"STACK_SEG ENDS" + newLine +
								newLine +
				"CODESC	SEGMENT 'CODE'" + newLine +
				"	ASSUME CS:CODESC, SS:STACK_SEG" + newLine +
								newLine +
				"BEGIN:	.386" + newLine;


			returnDOS =
				"MOV AH, 4CH" + newLine +
				"INT 21H" + newLine;


			asmFooter =
				"CODESC	ENDS" + newLine +
				"   END BEGIN" + newLine;


			scanfCode =
				"SCANF  PROC" + newLine +
				"MOV SI, 0	;флаг дали числото е отрицателно" + newLine +
				"    MOV EBX, 10" + newLine +
				"    MOV ECX, 0" + newLine +
				"M21:MOV AH, 0" + newLine +
				"    ;прочита се число от клавиатурата" + newLine +
				"    PUSH EBX" + newLine +
				"    MOV BH, AH" + newLine +
				"    MOV AH, 1" + newLine +
				"    INT 21H" + newLine +
				"    MOV AH, BH" + newLine +
				"    POP EBX" + newLine +
				"    ;край на четенето от клавиатурата" + newLine +

				"    CMP AL, 0DH	;дали е натиснат CR?" + newLine +
				"    JE M22" + newLine +

				"    ;проверка за въведен знак -" + newLine +
				"    CMP AL, 2DH" + newLine +
				"    JNE M20" + newLine +
				"    MOV SI, 1	;числото е отрицателно" + newLine +
				"    JMP M21" + newLine +

				"M20:SUB AL, 30H	;преобразуване на ASCII цифрата в двоична" + newLine +
				"    PUSH EAX	;съхраняване на последното въвеждане" + newLine +
				"    MOV EAX, ECX" + newLine +
				"    MUL EBX	;умножаване на десетичната сума с 10" + newLine +
				"    POP ECX	;последното въвеждане се извлича обратно" + newLine +
				"    ADD ECX, EAX	;в CX и се сумира" + newLine +
				"    JMP M21" + newLine +
				"    ;проверка дали числото е отрицателно (със знак - отпред)" + newLine +
				"M22:CMP SI, 1" + newLine +
				"    JNE M23" + newLine +
				"    NEG ECX" + newLine +
				";print new line (LF, CR)" + newLine +
				"M23:MOV DL, 0AH" + newLine +
				"	MOV AH,2" + newLine +
				"	INT 21h" + newLine +
				"	MOV DL, 0DH" + newLine +
				"	MOV AH,2" + newLine +
				"	INT 21h" + newLine +
				"	RET" + newLine +
				"SCANF  ENDP" + newLine;


			printfCode =
				"PRINTF PROC" + newLine +
				//проверка за отрицателно число
				"	    MOV    EBX, EAX" + newLine +
				"	    AND    EBX, 80000000H" + newLine +
				"	    CMP    EBX, 0" + newLine +
				"	    JZ	   M0" + newLine +

				"	    MOV    ECX, EAX" + newLine +

				"	    MOV    AH,	2" + newLine +
				"	    MOV    DL, 2DH" + newLine +
				"	    INT    21H" + newLine +

				"	    MOV    EDX, 0" + newLine +
				"	    SUB    EDX, ECX" + newLine +
				"	    MOV    EAX, EDX" + newLine +

				"M0:    MOV ECX, 0" + newLine +
				"	    MOV EBX, 10" + newLine +
				"M1:    MOV EDX, 0" + newLine +
				"	    DIV EBX" + newLine +
				"	    PUSH DX" + newLine +
				"	    INC ECX" + newLine +
				"	    CMP EAX, 0" + newLine +
				"	    JNZ M1" + newLine +
				"M2:    POP DX" + newLine +
				"	    OR DL, 30H" + newLine +
				"	    MOV AH,2" + newLine +
				"	    INT 21h" + newLine +

				"	    LOOP M2" + newLine +
				";print new line (LF, CR)" + newLine +
				"   MOV DL, 0AH" + newLine +
				"	MOV AH,2" + newLine +
				"	INT 21h" + newLine +
				"	MOV DL, 0DH" + newLine +
				"	MOV AH,2" + newLine +
				"	INT 21h" + newLine +
				"	RET" + newLine +
				"PRINTF ENDP" + newLine;
		}

		private static void includeProc(String proc_code)
		{
			//ако процедурата вече не е включена се включва
			if (!includedProcs.Contains(proc_code))
				includedProcs.Add(proc_code);
		}

		public static String getIncludedProcsCode()
		{
			String procs_code = "";
			IEnumerator<String> iter = includedProcs.GetEnumerator();
			while (iter.MoveNext())
				procs_code += iter.Current;
			return procs_code;
		}

		//взима следващия свободен адрес от стека на програмата,
		//където ще се съхраняват междинни резултати
		private static int getNextAvailableAddress()
		{
			//TODO: да работи заедно с дескрипторната таблица,
			//за да знае от кой адрес да започва да раздава
			nextAvailableAddress += 4;
			return nextAvailableAddress;
		}

		public static int getLastAddress()
		{
			return nextAvailableAddress;
		}

		public class Result
		{
			public String code;
			public bool savedIn;

			public const bool IN_MEMORY = true;
			public const bool IN_SAVE_PLACE = false;

			public Result(String fcode, bool finMemory)
			{
				code = fcode;
				savedIn = finMemory;
			}
		}

		private static String getAddressOrValue(Lexemе lexeme)
		{
			if (lexeme.id == Statement.intermediateLexemeSymbol)
			{
				//това е on-the-fly стойност
				return String.Format(savePlace, lexeme.address);
			}
			else if (lexeme.id == Lexemе.IDENTIFIER)
			{
				return String.Format("dword ptr [ebp+{0}]", lexeme.address);
			}
			else if (lexeme.id == Lexemе.NUMBER)
			{
				return lexeme.value.ToString();
			}
			else
				throw new TranslatorException("Error 1005: no match found for symbol: " +
					lexeme.id);
		}

		//генерира асемблерен код за единичен оператор
		public static Result processSingleOperation(Lexemе lexeme, Stack<Lexemе> stack)
		{
			switch (lexeme.id)
			{
				case Lexemе.PLUS:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doPlus(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.ASSIGNMENT:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Peek();
						return new Result(doAssignment(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_MEMORY);
					}
				case Lexemе.MINUS:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doMinus(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.PREFIX_INCREMENT:
				case Lexemе.POSTFIX_INCREMENT:
					{
						Lexemе only_side_lexeme = stack.Peek();
						return new Result(doIncrement(getAddressOrValue(only_side_lexeme)),
							Result.IN_MEMORY);
					}
				case Lexemе.PREFIX_DECREMENT:
				case Lexemе.POSTFIX_DECREMENT:
					{
						Lexemе only_side_lexeme = stack.Peek();
						return new Result(doDecrement(getAddressOrValue(only_side_lexeme)),
							Result.IN_MEMORY);
					}
				case Lexemе.BITWISE_INVERSION:
					{
						Lexemе only_side_lexeme = stack.Pop();
						return new Result(doBitwiseInversion(getAddressOrValue(only_side_lexeme)),
							Result.IN_SAVE_PLACE);
					}
				case Lexemе.BITWISE_AND:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doBitwiseAnd(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.BITWISE_OR:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doBitwiseOr(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.BOOLEAN_INVERSION:
					{
						Lexemе only_side_lexeme = stack.Pop();
						return new Result(doBooleanInversion(getAddressOrValue(only_side_lexeme)),
							Result.IN_SAVE_PLACE);
					}
				case Lexemе.UNARY_MINUS:
					{
						Lexemе only_side_lexeme = stack.Pop();
						return new Result(doUnaryMinus(getAddressOrValue(only_side_lexeme)),
							Result.IN_SAVE_PLACE);
					}
				case Lexemе.MULTUPLICATION:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doMultiplication(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.DIVISION:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doDivision(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.MODUL:
					{
						Lexemе right_side_lexeme = stack.Pop();
						Lexemе left_side_lexeme = stack.Pop();
						return new Result(doModul(getAddressOrValue(right_side_lexeme),
							getAddressOrValue(left_side_lexeme)), Result.IN_SAVE_PLACE);
					}
				case Lexemе.PRINTF:
					{
						Lexemе only_side_lexeme = stack.Pop();
						return new Result(doPrintf(getAddressOrValue(only_side_lexeme)),
							Result.IN_MEMORY);
					}
				case Lexemе.SCANF:
					{
						stack.Pop();
						return new Result(doScanf(),
							Result.IN_SAVE_PLACE);
					}
			}
			return null;
		}

		private static String getSavePlace()
		{
			return String.Format(savePlace, getNextAvailableAddress());
		}

		private static String doPlus(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;plus (3 rows)" + newLine +
				"add    eax, {1}" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doAssignment(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;assignment (2 rows)" + newLine +
				"mov    {1}, eax" + newLine,
				right_side, left_side);
			return code;
		}

		private static String doMinus(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;minus (3 rows)" + newLine +
				"sub    eax, {1}" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doIncrement(String only_side)
		{
			String code = String.Format(
				"inc    {0}	;increment (1 row)" + newLine,
				only_side);
			return code;
		}

		private static String doDecrement(String only_side)
		{
			String code = String.Format(
				"dec    {0}	;decrement (1 row)" + newLine,
				only_side);
			return code;
		}

		private static String doBitwiseInversion(String only_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;bitwise inversion (3 rows)" + newLine +
				"not    eax" + newLine +
				"mov    {1}, eax" + newLine,
				only_side, getSavePlace());
			return code;
		}

		private static String doBitwiseAnd(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;bitwise and (3 rows)" + newLine +
				"and    eax, {1}" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doBitwiseOr(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;bitwise or (3 rows)" + newLine +
				"or    eax, {1}" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doBooleanInversion(String only_side)
		{
			String code = String.Format(
				"xor    eax, eax	;boolean inversion (5 rows)" + newLine +
				"mov    ebx, {0}" + newLine +
				"cmp    ebx, 0" + newLine +
				"sete   al" + newLine +
				"mov    {1}, eax" + newLine,
				only_side, getSavePlace());
			return code;
		}

		private static String doUnaryMinus(String only_side)
		{
			String code = String.Format(
				"mov    eax, 0	;unary minus (3 rows)" + newLine +
				"sub    eax, {0}" + newLine +
				"mov    {1}, eax" + newLine,
				only_side, getSavePlace());
			return code;
		}

		private static String doMultiplication(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;multiplication (3 rows)" + newLine +
				"imul   eax, {1}" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doDivision(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;division (5 rows)" + newLine +
				"mov    ebx, {1}" + newLine +
				"cdq" + newLine +
				"idiv   ebx" + newLine +
				"mov    {2}, eax" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doModul(String right_side, String left_side)
		{
			String code = String.Format(
				"mov    eax, {0}	;modul (5 rows)" + newLine +
				"mov    ebx, {1}" + newLine +
				"cdq" + newLine +
				"idiv   ebx" + newLine +
				"mov    {2}, edx" + newLine,
				left_side, right_side, getSavePlace());
			return code;
		}

		private static String doPrintf(String only_side)
		{
			//тази функция очаква параметър в регистър AX
			includeProc(printfCode);
			String code = String.Format(
				"mov    eax, {0}	;printf (2 rows)" + newLine +
				"call   printf" + newLine,
				only_side);
			return code;
		}

		private static String doScanf()
		{
			//тази процедура връща резултат в регистър CX
			includeProc(scanfCode);
			String code = String.Format(
				"call   scanf	;scanf (2 rows)" + newLine +
				"mov    {0}, ECX" + newLine,
				getSavePlace());
			return code;
		}
	}
}
