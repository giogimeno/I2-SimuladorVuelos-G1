using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Atributos

        string id; // identificador
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;

        // Constructures
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        // Metodos (SETTERS AND GETTERS)
        public Position GetActualPosition()
        { return this.currentPosition; }

        public Position GetFinalPosition()
        { return this.finalPosition; }

        public string GetId()
        { return this.id; }

        public double GetVelocidad()
        { return this.velocidad; }

        public void SetVelocidad(double velocidad)
        // setter del atributo velocidad
        { this.velocidad = velocidad; }

        public void Mover(double tiempo)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            currentPosition = new Position(x, y);
        }

        public bool DidFlightArrive()
        {
            double thereshold = 10;
            double distancia = this.GetActualPosition().Distancia(this.GetFinalPosition());
            if (distancia < thereshold)
            {
                Console.WriteLine("El vuelo ha llegado a su destino.");
                return true;
            }
            else
            {
                Console.WriteLine("El vuelo aún no ha llegado a su destino.");
                return false;
            }
        }

        public void BucleSimulacionHastaCiclos (int ciclos)
        {
            this.Mover(ciclos);
            this.EscribeConsola();
            Console.WriteLine("Se ha movido el vuelo durante {0} ciclos", ciclos);
        }

        public void BucleSimulacionHastaFinal (int ciclos)
        {
            bool final = false;
            int contador = 0;
            while (contador < ciclos && final == false)
            {
                this.Mover(1);
                contador += 1;
                final = this.DidFlightArrive();
            }
            Console.WriteLine("Se ha movido el vuelo durante {0} ciclos", contador);
        }

        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0}", velocidad);
            Console.WriteLine("Posición actual: ({0},{1})", currentPosition.GetX(), currentPosition.GetY());
            Console.WriteLine("******************************");
        }

        public bool HayColisionConElVuelo(FlightPlan secundario, double distancia_minima)
        {
            double distancia = this.GetActualPosition().Distancia(secundario.GetActualPosition());
            if (distancia < distancia_minima)
            {
                Console.WriteLine("Hay colisión entre los vuelos {0} y {1}", this.GetId(), secundario.GetId());
                return true;
            }
            else
            {
                Console.WriteLine("No hay colisión entre los vuelos {0} y {1}", this.GetId(), secundario.GetId());
                return false;
            }
        }
    }
}