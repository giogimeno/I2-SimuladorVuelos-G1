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
        Dot[] listaPuntos = new Dot[1000];
        int num = 0;

        public void AddDot(Dot dot)
        {
            this.listaPuntos[this.num] = dot; this.num++; 
        }

        public Dot GetDot(int i)
        {
            return this.listaPuntos[i]; 
        }

        public int GetNum()
        { 
            return this.num; 
        }

    }
}
