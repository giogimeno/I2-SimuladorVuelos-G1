using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] lista = new FlightPlan[100];
        double tiempo = 0;
        double distanciaSeguridad = 0;
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

        public void SetDistanciaSeguridad(double distancia)
        {
            this.distanciaSeguridad = distancia;
        }

        public double GetDistanciaSeguridad()
        {
            return distanciaSeguridad;
        }

        public void SetTiempo(double tiempo)
        {
            this.tiempo = tiempo;
        }

        public double GetTiempo()
        {
            return tiempo;
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

        public int CargarArchivo(string filename)
        {
            try
            {
                StreamReader file = new StreamReader(filename);
                string line = file.ReadLine();
                string[] trozos = line.Split('/');

                if (trozos.Length != 2)
                {
                    return -1;

                }

                this.tiempo = Convert.ToDouble(trozos[0]);
                this.distanciaSeguridad = Convert.ToDouble(trozos[1]);



                line = file.ReadLine();

                while (line != null)
                {
                    trozos = line.Split('/');

                    if (trozos.Length != 6)
                        return -1;

                    else
                    {
                        try
                        {
                            string identificador = trozos[0];
                            double pos0X = Convert.ToDouble(trozos[1]);
                            double pos0Y = Convert.ToDouble(trozos[2]);
                            double posFX = Convert.ToDouble(trozos[3]);
                            double posFY = Convert.ToDouble(trozos[4]);
                            double velocidad = Convert.ToDouble(trozos[5]);

                            FlightPlan plan = new FlightPlan(identificador, pos0X, pos0Y, posFX, posFY, velocidad);

                            AddFlightPlan(plan);
                        }

                        catch (FormatException)
                        {
                            return -1;
                        }
                    }

                    line = file.ReadLine();
                }

                file.Close();
                return 0;
            }

            catch (FileNotFoundException)
            {
                return -2;
            }

        }

        public int GuardarPlanes(string filename)
        {
            try
            {
                StreamWriter file = new StreamWriter(filename);
                file.WriteLine(tiempo + "/" + distanciaSeguridad);

                int i = 0;

                while (i < number)
                {
                    file.WriteLine(lista[i].GetId() + "/" + lista[i].GetActualPosition().GetX() + "/" + lista[i].GetActualPosition().GetY() +
                                                 "/" + lista[i].GetFinalPosition().GetX() + "/" + lista[i].GetFinalPosition().GetY() +
                                                 "/" + lista[i].GetVelocidad());
                    i++;

                }

                file.Close();
                return 0;
            }

            catch (FileNotFoundException)
            {
                return -1;
            }

        }

    }

 
}
