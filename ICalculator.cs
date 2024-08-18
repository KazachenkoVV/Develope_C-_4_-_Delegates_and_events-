using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInterface
{
    internal interface ICalculator
    {
        event EventHandler<MyEventArgs> GotResult;
        void Add(decimal i);
        void Sub(decimal i);
        void Div(decimal i);
        void Mul(decimal i);
        void CancelLast();
    }
}
