using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[10];
        int number = 0;

        public int AddFlightPlan(FlightPlan p)
        {
            if (number <= 10)
            {
                vector[number] = p;
                number++;
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            {
                return null;
            }
            else
            {
                return vector[i];
            }
        }
        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                i++;
            }
        }
        public void EscribeConsola() 
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }
        }
        public bool CheckColisions(int DistanciaMinima)
        {
            int i = 0;
            while (i < number)
            {
                int j = 0;
                while (j < number && i != j)
                {
                    if (vector[i].HayColisionConElVuelo(vector[j], DistanciaMinima))
                    {
                        return true;
                    }
                    else { return false; }
                    j++;
                }
                i++;
            }
            return false;
        }
    }
}
