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
        Position initialPosition; // posicion inicial
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;

        // Constructor
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        // Sets & Gets
        public Position GetActualPosition()
        { return this.currentPosition; }

        public Position GetFinalPosition()
        { return this.finalPosition; }

        public Position GetInitialPosition()
        { return this.initialPosition; }

        public string GetId()
        { return this.id; }

        public double GetVelocidad()
        { return this.velocidad; }

        public void SetVelocidad(double velocidad)
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

        public bool HasArrived()
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

        public double Distancia(FlightPlan secundario)
        {
            double distancia = this.GetActualPosition().Distancia(secundario.GetActualPosition());
            return distancia;
        }
        public bool HayColisionConElVuelo(FlightPlan secundario, double distancia_minima)
        {
            if (this.Distancia(secundario) < distancia_minima)
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

        public void Restart()
        { this.currentPosition = this.initialPosition; }

        public double GetTiempoHastaDestino()
        {
            double distanciaRestante = this.GetActualPosition().Distancia(this.GetFinalPosition());
            if (velocidad <= 0)
            {
                return 0;
            }
            return distanciaRestante / velocidad * 60; // tiempo en minutos
        }

        public Position GetPosicionFutura(double minutos)
        {
            Position actual = this.GetActualPosition();
            Position destion = this.GetFinalPosition();
            double distanciaRestante = actual.Distancia(destion);

            if (distanciaRestante == 0 || velocidad <= 0)
            {
                return new Position(destion.GetX(), destion.GetY());
            }

            double distanciaRecorrida = velocidad * minutos / 60;

            if (distanciaRecorrida >= distanciaRestante)
            {
                return new Position(destion.GetX(), destion.GetY());
            }

            double proporcion = distanciaRecorrida / distanciaRestante;

            double x = actual.GetX() + (destion.GetX() - actual.GetX()) * proporcion;
            double y = actual.GetY() + (destion.GetY() - actual.GetY()) * proporcion;

            return new Position(x, y);
        }
    }
}