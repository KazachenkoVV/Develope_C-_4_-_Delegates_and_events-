// Доработайте  программу  калькулятор  реализовав  выбор  действий  и  вывод  результатов  на 
// экран в цикле так чтобы калькулятор мог работать до тех пор пока пользователь не нажмет 
// отмена или введёт пустую строку.

namespace CalculatorInterface
{
    internal class Program
    {
        static void Calculator_GotResult(object sender, MyEventArgs e)
        {
            Console.WriteLine($"{e.textArg} = {((Calculator)sender).result}");
        }
        static void Calculator_Undo(object sender, EventArgs e)
        {
            Console.WriteLine($"Отмена операции = {((Calculator)sender).result}");
        }
        delegate void Operator(decimal x);
        private static void BackSpace()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("ПРОГРАММА - КАЛЬКУЛЯТОР");
            Console.WriteLine("Управление");
            Console.WriteLine("Арифметические действия:   [+] [-] [*] [/]");
            Console.WriteLine("Ввод аргументов:           [0] [1] [2] [3] [4] [5] [6] [7] [8] [9] [.] [,]");
            Console.WriteLine("Корректировка ввода:       [<-BackSpace]");
            Console.WriteLine("Отмена последней операции: [Del]");
            Console.WriteLine("Завершение работы:         [Space]");
            Calculator calc = new Calculator();
            calc.GotResult += Calculator_GotResult;
            calc.UndoOperation += Calculator_Undo;
            decimal arg = 0;
            string stringArg = "";
            int decimalPlace = 0;
            bool point = false;
            Console.WriteLine("\n" + arg);
            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar == ' ')    // Выход из программы
                    break;
                else if (keyInfo.Key == ConsoleKey.Delete) // Отмена последней операции
                    calc.CancelLast();
                else if (char.IsDigit(keyInfo.KeyChar))  // Обработка нажатия цифровых клавиш
                {
                    Console.Write(keyInfo.KeyChar);
                    if (decimalPlace == 0)
                        arg = arg * 10 + (decimal)char.GetNumericValue(keyInfo.KeyChar);
                    else
                        arg = arg + (decimal)(char.GetNumericValue(keyInfo.KeyChar) / Math.Pow(10, decimalPlace++));
                    if (point)
                        point = false;
                }
                else if (keyInfo.KeyChar == ',' || keyInfo.KeyChar == '.') // Обработка ввода десятичной точки
                {
                    if (decimalPlace == 0)
                    {
                        Console.Write(keyInfo.KeyChar);
                        point = true;
                        decimalPlace = 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace) // Обработка клавиши [Backspace]
                {
                    if (Console.CursorLeft > 0)
                    {
                        if (point)
                        {
                            BackSpace();
                            point = false;
                            decimalPlace = 0;
                        }
                        else
                        {
                            if (arg == 0)
                                BackSpace();
                            else
                            {
                                if (decimalPlace == 0)
                                {
                                    arg = (decimal)(int)(arg / 10);
                                    BackSpace();
                                }
                                else if (decimalPlace == 2)
                                {
                                    arg = (decimal)(int)arg;
                                    decimalPlace--;
                                    point = true;
                                    BackSpace();
                                }
                                else
                                {
                                    stringArg = arg.ToString();
                                    if (decimal.TryParse((stringArg.Remove(stringArg.Length - 1)), out decimal number))
                                    {
                                        arg = number;
                                        decimalPlace--;
                                    }
                                    else
                                        arg = 0;
                                    BackSpace();
                                }
                            }
                        }
                    }
                }
                else if ("+-*/".Contains(keyInfo.KeyChar))  // Обработка клавиш арифметических операций
                {
                    Console.Write(" " + keyInfo.KeyChar);
                    Console.Beep();
                    System.Threading.Thread.Sleep(1000);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Operator oper = SelectOperator(keyInfo);
                    oper((decimal)arg);
                    arg = 0;
                    decimalPlace = 0;
                }
            }
            Operator SelectOperator(ConsoleKeyInfo keyInfo)
            {
                switch (keyInfo.KeyChar)
                {
                    case '+':
                        return calc.Add;
                    case '-':
                        return calc.Sub;
                    case '/':
                        return calc.Div;
                    case '*':
                        return calc.Mul;
                    default:
                        throw new ArgumentException("Калькулятор не может выполнить такую операцию!");
                }
            }
        }
    }
}