using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInterface
{
    internal class MyEventArgs: EventArgs
    {
        public Operator Operator { get; }
        public string textArg { get; }
        public decimal arg1 { get; }
        public decimal arg2 { get; }
        public MyEventArgs(Operator Operator, decimal arg1, decimal arg2)
        {
            switch (Operator)
            {
                case Operator.Add:
                    textArg = $"{arg1} + {arg2}";
                    break;
                case Operator.Sub:
                    textArg = $"{arg1} - {arg2}";
                    break;
                case Operator.Mul:
                    textArg = $"{arg1} * {arg2}";
                    break;
                case Operator.Div:
                    textArg = $"{arg1} / {arg2}";
                    break;
                default:
                    throw new ArgumentException("Несуществующий символ операции!"); ;
            }
        }
    }
}
