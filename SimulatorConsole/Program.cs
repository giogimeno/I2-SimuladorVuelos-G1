using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using FlightLib;

namespace SimulatorConsole
{   
    public class Program
    {
        static FlightPlan CrearFlightPlan()
        {
            bool creado = false;
            while (creado == false)
            {
                try
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("  CREAR UN NUEVO FLIGHT PLAN");
                    Console.WriteLine("******************************");
                    Console.WriteLine("Escribe el identificador");
                    //   string nombre = Console.ReadLine();
                    string identificador = Console.ReadLine(); ;

                    Console.WriteLine("Escribe la velocidad");
                    double velocidad = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                    string linea = Console.ReadLine();
                    string[] trozos = linea.Split(' ');

                    double ix = Convert.ToDouble(trozos[0]);
                    double iy = Convert.ToDouble(trozos[1]);


                    Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                    linea = Console.ReadLine();
                    trozos = linea.Split(' ');
                    double fx = Convert.ToDouble(trozos[0]);
                    double fy = Convert.ToDouble(trozos[1]);

                    FlightPlan plan_a = new FlightPlan(identificador, ix, iy, fx, fy, velocidad);
                    creado = true;
                    Console.WriteLine("Se ha creado el Flight Plan Correctamente");
                    return plan_a;
                }

                catch (FormatException)
                {
                    Console.WriteLine("Error: Formato de datos incorrectos.");
                    return null;
                }
            }
            return null;
        }

        /////////////////////////////////// PROGRAMA PRINCIPAL //////////////////////////////////////////////////////
        static void Main(string[] args)
        {

            FlightPlan plan_a = CrearFlightPlan() ;
            while (plan_a == null)
            {
                Console.WriteLine("No se pudo crear el FlightPlan A debido a errores de formato. Intentar nuevamente.");
                plan_a = CrearFlightPlan();                
            }
            FlightPlan plan_b = CrearFlightPlan();
            while (plan_b == null)
            {
                Console.WriteLine("No se pudo crear el FlightPlan B debido a errores de formato. Intentar nuevamente.");
                plan_b = CrearFlightPlan();
            }

            FlightPlan plan_d = new FlightPlan("A", 0, 0, 100, 100, 10);
            FlightPlan plan_e = new FlightPlan("B", 100, 100, 0, 0, 10);
            FlightPlan plan_c = new FlightPlan("C", 0, 100, 100, 0, 500);

            FlightPlanList lista = new FlightPlanList();
            lista.AddFlightPlan(plan_a);
            lista.AddFlightPlan(plan_b);
            lista.AddFlightPlan(plan_c);
            lista.AddFlightPlan(plan_d);
            lista.AddFlightPlan(plan_e);
            // Ya navegamos directo con clase de array, no con array directo

            lista.EscribeConsola();
                                  
            int ciclos = 0;
            int Tciclo = 5;
            int DistanciaMinima = 10;
            while (ciclos < 1000 && !lista.CheckColisions(DistanciaMinima))
            {
                lista.Mover(Tciclo);
                ciclos++;
            }
            Console.WriteLine("Simulación finalizada. Se han realizado {0} ciclos.", ciclos);

            
        }   
    }
}

// Escribir si un vuelo HA LLEGADO a su destino (Funcion DidFlightArrive) OK, check umbral de seguridad
// Modificar MoverVuelo para que no se pase de su destino (Funcion Mover Revisarla, de momento esta hecho el bucle hasta ciclos)
// Metodo de conflictos con distancia de seguridad (HayColisionConElVuelo) OK