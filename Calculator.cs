using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInterface
{
    internal class Calculator : ICalculator
    {
        public event EventHandler<MyEventArgs> ?GotResult;
        public event EventHandler<EventArgs> ?UndoOperation;
        public decimal result = 0;
        Stack<decimal> history = new Stack<decimal>();

        /// <summary>
        /// Прибавляет аргумент к результату последней операции.
        /// </summary>
        /// <param name="arg">Аргумент прибавляемый к рузультату последенй операции.</param>
        /// <returns>Сумму.</returns>
        public void Add(decimal arg)
        {
            history.Push(result);
            result += arg;
            RaiseEvent(new MyEventArgs (Operator.Add, history.Peek(), arg));
        }

        public void Div(decimal arg)
        {
            if (arg == 0)
                throw new ArgumentException("Деление на ноль недопустимо.");
            else
            {
                history.Push(result);
                result /= arg;
                RaiseEvent(new MyEventArgs(Operator.Div, history.Peek(), arg));
            }
        }

        public void Mul(decimal arg)
        {
            history.Push(result);
            result *= arg;
            RaiseEvent(new MyEventArgs(Operator.Mul, history.Peek(), arg));
        }

        public void Sub(decimal arg)
        {
            history.Push(result); 
            result -= arg;
            RaiseEvent(new MyEventArgs(Operator.Sub, history.Peek(), arg));
        }
        /// <summary>
        /// Отменяет последнюю операцию.
        /// </summary>
        public void CancelLast()
        {
            if (history.Count > 0)
                result = history.Pop();
            UndoEvent();
        }
        private void RaiseEvent(MyEventArgs e)
        {
            GotResult?.Invoke(this, e);
        }
        private void UndoEvent()
        {
            UndoOperation?.Invoke(this, EventArgs.Empty);
        }

    }
}
