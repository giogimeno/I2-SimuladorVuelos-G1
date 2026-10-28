using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] lista = new FlightPlan[10];
        int number = 0;

        public int AddFlightPlan(FlightPlan p)
        {
            if (number <= 10)
            {
                lista[number] = p;
                number++;
                return 0;
            }
            else
            { return -1; }
        }

        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            { return null; }
            else
            { return lista[i]; }
        }

        public int GetNum()
        { return this.number; }

        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < number)
            {
                lista[i].Mover(tiempo);
                i++;
            }
        }

        public void EscribeConsola() 
        {
            int i = 0;
            while (i < number)
            {
                lista[i].EscribeConsola();
                i++;
            }
        }

    }
}
