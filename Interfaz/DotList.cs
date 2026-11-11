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
        List<Dot> listaPuntos = new List<Dot>();

        public void AddDot(Dot dot)
        { this.listaPuntos.Add(dot); }

        public Dot GetDot(int i)
        { return this.listaPuntos[i]; }

        public int GetNum()
        { return this.listaPuntos.Count; }

    }
}
