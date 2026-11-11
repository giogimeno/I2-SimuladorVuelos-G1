using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        // Lista de Planes de vuelo, Stack de Colas de posiciones, Tiempo y Distancia.
        List<FlightPlan> lista = new List<FlightPlan>();
        
        PositionStack StackPosiciones = new PositionStack();

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

        public int Retroceder(double tiempo)
        {
            Queue<Position> posicionesRetrocedidas = StackPosiciones.RecuperarPosicionesAnteriores();
            if (posicionesRetrocedidas != null)
            {
                foreach (FlightPlan plan in lista) plan.SetActualPosition(posicionesRetrocedidas.Dequeue());
                return 0;
            }
            return -1;
        }

        public void Mover(double tiempo)
        {
            Queue<Position> posicionesActuales = new Queue<Position>();

            foreach (FlightPlan plan in lista)
            {
                posicionesActuales.Enqueue(plan.GetActualPosition());

                plan.Mover(tiempo);
            }
            StackPosiciones.AñadirPosicionesNuevas(posicionesActuales);
        }

        public void Reset()
        {
            foreach (FlightPlan plan in lista) plan.Restart();
            StackPosiciones.Clear();
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
            foreach (FlightPlan plan in lista)
            {
                plan.EscribeConsola();
            }
        }


        // Funciones que forman parte de la Carga de Archivos

        /// <summary>
        /// Carga los planes de vuelo del archivo de texto en la lista.
        /// </summary>
        /// <param name="filename">Nombre del archivo a leer</param>
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


        // Funciones que forman parte del Gestor de Conflictos
        public bool ExisteConflictoConCualquiera()
        {
            double margenSeguridad = this.distanciaSeguridad / 2.0;
            for (int indiceAvion1 = 0; indiceAvion1 < this.lista.Count; indiceAvion1++)
            {
                FlightPlan avion = this.lista[indiceAvion1];

                for (int indiceAvion2 = 0; indiceAvion2 < this.lista.Count; indiceAvion2++)
                {
                    if (indiceAvion2 != indiceAvion1)
                    {
                        ResultadoPrediccionColisiones res = avion.PredecirConflicto(this.lista[indiceAvion2], margenSeguridad);
                        if (res.HayColision) return true;
                    }
                }
            }
            
            return false;
        }

        public FlightPlan IntentarResolverConflictos()
        {
            double margenSeguridad = this.GetDistanciaSeguridad() / 2.0;
            FlightPlan avionResuelto = null;

            for (int i = 0; i < this.GetNum(); i++)
            {
                for (int j = i + 1; j < this.GetNum(); j++)
                {
                    FlightPlan avion1 = this.GetFlightPlan(i);
                    FlightPlan avion2 = this.GetFlightPlan(j);

                    ResultadoPrediccionColisiones res = avion1.PredecirConflicto(avion2, margenSeguridad);

                    if (res.HayColision)
                    {
                        double velocidadOriginal = avion1.GetVelocidad();
                        bool solucionado = false;

                        // Intentamos reducir la velocidad...
                        for (double factor = 0.995; factor >= 0.50; factor -= 0.005)
                        {
                            double pruebaVelocidad = velocidadOriginal * factor;
                            avion1.SetVelocidad(pruebaVelocidad);

                            if (!this.ExisteConflictoConCualquiera())
                            {
                                avionResuelto = avion1;
                                solucionado = true;
                                break;
                            }
                        }

                        // Si seguimos sin haberlo solucionado, aumentamos velocidad...
                        if (solucionado == false)
                        {
                            for (double factor = 1.005; factor <= 1.50; factor += 0.005)
                            {
                                double pruebaVelocidad = velocidadOriginal * factor;
                                avion1.SetVelocidad(pruebaVelocidad);

                                if (!this.ExisteConflictoConCualquiera())
                                {
                                    avionResuelto = avion1;
                                    solucionado = true;
                                    break;
                                }
                            }
                        }

                        // Si no hay solución para este avión, restaurar su velocidad inicial
                        if (solucionado == false) avion1.SetVelocidad(velocidadOriginal);
                        else break;
                    }
                }
                if (avionResuelto != null) break; // Detener la búsqueda al encontrar la primera solución
            }

            return avionResuelto;
        }



    }
}
