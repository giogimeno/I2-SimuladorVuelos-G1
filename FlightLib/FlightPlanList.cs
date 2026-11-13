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

        /// <summary>
        /// Cuenta cuántas parejas de vuelos están en conflicto (cada pareja se cuenta una sola vez).
        /// </summary>
        public int ContarConflictos()
        { double margenSeguridad = this.distanciaSeguridad / 2.0;
            int conflictos = 0;
            for (int i = 0; i < this.lista.Count; i++)
            {
                for (int j = i + 1; j < this.lista.Count; j++)
                {
                    ResultadoPrediccionColisiones res = this.lista[i].PredecirConflicto(this.lista[j], margenSeguridad);
                    if (res.HayColision)
                    { conflictos++; }
                }
            }
            return conflictos;
        }

        /// <summary>
        /// Devuelve una lista con las parejas de vuelos en conflicto, por ejemplo "A1 - A2".
        /// </summary>

        public List<string> ObtenerParejasEnConflicto()
        {
            double margenSeguridad = this.distanciaSeguridad / 2.0;
            List<string> parejas = new List<string>();
            for (int i = 0; i < this.lista.Count; i++)
            {
                for (int j = i + 1; j < this.lista.Count; j++)
                {
                    ResultadoPrediccionColisiones res = this.lista[i].PredecirConflicto(this.lista[j], margenSeguridad);
                    if (res.HayColision)
                    {
                        parejas.Add(this.lista[i].GetId() + "-" + this.lista[j].GetId());
                    }
                }
            }
            return parejas;
        }

        //public bool ExisteConflictoConCualquiera()
        //{
        //    double margenSeguridad = this.distanciaSeguridad / 2.0;
        //    for (int indiceAvion1 = 0; indiceAvion1 < this.lista.Count; indiceAvion1++)
        //    {
        //        FlightPlan avion = this.lista[indiceAvion1];

        //        for (int indiceAvion2 = 0; indiceAvion2 < this.lista.Count; indiceAvion2++)
        //        {
        //            if (indiceAvion2 != indiceAvion1)
        //            {
        //                ResultadoPrediccionColisiones res = avion.PredecirConflicto(this.lista[indiceAvion2], margenSeguridad);
        //                if (res.HayColision) return true;
        //            }
        //        }
        //    }
            
        //    return false;
        //}

        public bool ExisteConflictoConCualquiera()
        {
            return this.ContarConflictos() > 0;
        }

        private bool ParticipaEnConflicto(int indice)
        {
            double margenSeguridad = this.distanciaSeguridad / 2.0;

            for (int j = 0; j < this.lista.Count; j++)
            {
                if (j != indice)
                {
                    ResultadoPrediccionColisiones res = this.lista[indice].PredecirConflicto(this.lista[j], margenSeguridad);
                    if (res.HayColision)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private int ContarConflictosConVelocidad(int indice, double velocidad)
        {
            double velocidadActual = this.lista[indice].GetVelocidad();

            this.lista[indice].SetVelocidad(velocidad);
            int conflictos = this.ContarConflictos();
            this.lista[indice].SetVelocidad(velocidadActual);

            return conflictos;
        }
        private bool AplicarMejorCambio(List<double> velocidadesOriginales, int conflictosActuales)
        {
            int mejorAvion = -1;
            double mejorVelocidad = 0;
            int mejorConflictos = conflictosActuales;

            for (int i = 0; i < this.lista.Count; i++)
            {
                // Solo probamos con los vuelos que están en conflicto
                if (this.ParticipaEnConflicto(i))
                {
                    // Probamos cambios cada vez mayores: 0,5%, 1%, 1,5% ... 50%
                    for (int paso = 1; paso <= 100; paso++)
                    {
                        double cambio = paso * 0.005;

                        double velocidadMenor = velocidadesOriginales[i] * (1 - cambio);
                        double velocidadMayor = velocidadesOriginales[i] * (1 + cambio);

                        int conflictosMenor = this.ContarConflictosConVelocidad(i, velocidadMenor);
                        int conflictosMayor = this.ContarConflictosConVelocidad(i, velocidadMayor);

                        // Solo guardamos la solución si mejora estrictamente,
                        // así nos quedamos con el cambio más pequeño posible
                        if (conflictosMenor < mejorConflictos)
                        {
                            mejorConflictos = conflictosMenor;
                            mejorAvion = i;
                            mejorVelocidad = velocidadMenor;
                        }
                        if (conflictosMayor < mejorConflictos)
                        {
                            mejorConflictos = conflictosMayor;
                            mejorAvion = i;
                            mejorVelocidad = velocidadMayor;
                        }
                    }
                }
            }

            if (mejorAvion == -1)
            {
                return false; // ningún cambio mejora la situación
            }

            this.lista[mejorAvion].SetVelocidad(mejorVelocidad);
            return true;
        }

        //public FlightPlan IntentarResolverConflictos()
        //{
        //    double margenSeguridad = this.GetDistanciaSeguridad() / 2.0;
        //    FlightPlan avionResuelto = null;

        //    for (int i = 0; i < this.GetNum(); i++)
        //    {
        //        for (int j = i + 1; j < this.GetNum(); j++)
        //        {
        //            FlightPlan avion1 = this.GetFlightPlan(i);
        //            FlightPlan avion2 = this.GetFlightPlan(j);

        //            ResultadoPrediccionColisiones res = avion1.PredecirConflicto(avion2, margenSeguridad);

        //            if (res.HayColision)
        //            {
        //                double velocidadOriginal = avion1.GetVelocidad();
        //                bool solucionado = false;

        //                // Intentamos reducir la velocidad...
        //                for (double factor = 0.995; factor >= 0.50; factor -= 0.005)
        //                {
        //                    double pruebaVelocidad = velocidadOriginal * factor;
        //                    avion1.SetVelocidad(pruebaVelocidad);

        //                    if (!this.ExisteConflictoConCualquiera())
        //                    {
        //                        avionResuelto = avion1;
        //                        solucionado = true;
        //                        break;
        //                    }
        //                }

        //                // Si seguimos sin haberlo solucionado, aumentamos velocidad...
        //                if (solucionado == false)
        //                {
        //                    for (double factor = 1.005; factor <= 1.50; factor += 0.005)
        //                    {
        //                        double pruebaVelocidad = velocidadOriginal * factor;
        //                        avion1.SetVelocidad(pruebaVelocidad);

        //                        if (!this.ExisteConflictoConCualquiera())
        //                        {
        //                            avionResuelto = avion1;
        //                            solucionado = true;
        //                            break;
        //                        }
        //                    }
        //                }

        //                // Si no hay solución para este avión, restaurar su velocidad inicial
        //                if (solucionado == false) avion1.SetVelocidad(velocidadOriginal);
        //                else break;
        //            }
        //        }
        //        if (avionResuelto != null) break; // Detener la búsqueda al encontrar la primera solución
        //    }

        //    return avionResuelto;
        //}

        public int IntentarResolverConflictos()
        {
            // 1) Guardamos las velocidades originales para no salirnos nunca del margen 50% - 150%
            List<double> velocidadesOriginales = new List<double>();
            foreach (FlightPlan plan in this.lista)
            {
                velocidadesOriginales.Add(plan.GetVelocidad());
            }

            // 2) Lista de factores a probar, ordenados de menor a mayor cambio:
            //    0.995, 1.005, 0.990, 1.010, ... 0.500, 1.500
            //    Así, ante dos soluciones igual de buenas, nos quedamos con la que cambia menos la velocidad.
            List<double> factores = new List<double>();
            for (int paso = 1; paso <= 100; paso++)
            {
                factores.Add(1 - paso * 0.005);
                factores.Add(1 + paso * 0.005);
            }

            // 3) Mientras queden conflictos y todavía podamos mejorar, buscamos el mejor cambio
            int conflictosActuales = this.ContarConflictos();
            bool mejora = true;

            while (conflictosActuales > 0 && mejora)
            {
                mejora = false;
                int mejorAvion = -1;          // posición del vuelo que mejor resuelve
                double mejorVelocidad = 0;    // velocidad que hay que ponerle
                int mejorConflictos = conflictosActuales;

                for (int i = 0; i < this.lista.Count; i++)
                {
                    // Solo probamos con los vuelos que están en conflicto
                    if (this.ParticipaEnConflicto(i))
                    {
                        double velocidadActual = this.lista[i].GetVelocidad();

                        foreach (double factor in factores)
                        {
                            double pruebaVelocidad = velocidadesOriginales[i] * factor;
                            this.lista[i].SetVelocidad(pruebaVelocidad);

                            int conflictos = this.ContarConflictos();
                            if (conflictos < mejorConflictos)
                            {
                                mejorConflictos = conflictos;
                                mejorAvion = i;
                                mejorVelocidad = pruebaVelocidad;
                            }
                        }

                        // Dejamos el vuelo como estaba antes de las pruebas
                        this.lista[i].SetVelocidad(velocidadActual);
                    }
                }

                // 4) Si hemos encontrado un cambio que mejora, lo aplicamos y volvemos a empezar
                if (mejorAvion != -1)
                {
                    this.lista[mejorAvion].SetVelocidad(mejorVelocidad);
                    conflictosActuales = mejorConflictos;
                    mejora = true;
                }
            }

            return conflictosActuales;
        }
    }
}
