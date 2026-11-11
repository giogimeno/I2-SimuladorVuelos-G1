using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    internal class PositionStack
    {
        Stack<Queue<Position>> stack = new Stack<Queue<Position>>();

        public void AñadirPosicionesNuevas(Queue<Position> positions)
        {
            stack.Push(positions);
        }

        public Queue<Position> RecuperarPosicionesAnteriores()
        {
            if (stack.Count > 0)
            { return stack.Pop(); }

            else return null;
        }

        public void Clear()
        {
            stack.Clear();
        }
    }
}
