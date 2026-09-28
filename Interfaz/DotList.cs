using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLib;

namespace Interfaz
{
    internal class DotList
    {
        Dot[] listapuntos = new Dot[1000];
        int num = 0;

        public void AddDot(Dot dot)
        { this.listapuntos[this.num] = dot; this.num++; }

        public Dot GetDot(int i)
        { return this.listapuntos[i]; }

        public int GetNum()
        { return this.num; }

    }
}
