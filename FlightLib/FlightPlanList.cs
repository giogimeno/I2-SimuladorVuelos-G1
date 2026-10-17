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
            { return -1; }
        }

        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            { return null; }
            else
            { return vector[i]; }
        }

        public int GetNum()
        { return this.number; }

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
                    j++;
                }
                i++;
            }
            return false;
        }

        public bool CheckColisionesFuturas(double distanciaMinima)
        {
            double tiempoMaximo = 0;
            for (int i = 0; i < number; i++)
            {
                double tiempoHastaDestino = vector[i].GetTiempoHastaDestino();
                if (tiempoHastaDestino > tiempoMaximo)
                {
                    tiempoMaximo = tiempoHastaDestino;
                }
            }

            for (double t = 0; t <= tiempoMaximo; t += 1) //Tienes una funcion (HayColisionConElVuelo) que te permite
                                                          //comprobar si dos vuelos colisionan en un instante de tiempo determinado.
                                                          //Puedes usarla para comprobar si hay colisiones futuras
                                                          //entre todos los vuelos en la lista.
            {
                for (int i = 0; i < number; i++)
                {
                    for (int j = i + 1; j < number; j++)
                    {
                        Position pos1 = vector[i].GetPosicionFutura(t);
                        Position pos2 = vector[j].GetPosicionFutura(t);
                        double distancia = pos1.Distancia(pos2);
                        if (distancia < distanciaMinima)
                        {
                            Console.WriteLine($"Colisión futura detectada entre los vuelos {vector[i].GetId()} y {vector[j].GetId()} en el tiempo {t} minutos.");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}
