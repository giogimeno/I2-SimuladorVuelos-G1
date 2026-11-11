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
        List<FlightPlan> lista = new List<FlightPlan>();
        double tiempo = 0;
        double distanciaSeguridad = 0;

        public int AddFlightPlan(FlightPlan p)
        {
            lista.Add(p);
            return 0;
        }

        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= lista.Count)
            { return null; }
            else
            { return lista[i]; }
        }

        public int GetNum()
        { return this.lista.Count; }

        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < lista.Count)
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
            while (i < lista.Count)
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

                while (i < lista.Count)
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
