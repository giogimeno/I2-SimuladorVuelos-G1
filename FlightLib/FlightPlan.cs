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
        Position currentPosition; // posicion PosActual
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

        public double GetVelocidadX()
        {
            double dX = this.finalPosition.GetX() - this.currentPosition.GetX();
            double dY = this.finalPosition.GetY() - this.currentPosition.GetY();
            double distanciaTotal = Math.Sqrt(dX * dX + dY * dY);

            if (distanciaTotal == 0)
            { return 0; }

            return (dX / distanciaTotal) * this.velocidad;
        }

        public double GetVelocidadY()
        {
            double dX = this.finalPosition.GetX() - this.currentPosition.GetX();
            double dY = this.finalPosition.GetY() - this.currentPosition.GetY();
            double distanciaTotal = Math.Sqrt(dX * dX + dY * dY);

            if (distanciaTotal == 0)
            {
                return 0;
            }

            return (dY / distanciaTotal) * this.velocidad;
        }

        public void SetVelocidad(double velocidad)
        { this.velocidad = velocidad; }

        public void Mover(double tiempo)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            if (distancia != 0)
            {
                //Calculamos las razones trigonométricas
                double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
                double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
                double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

                //Caculamos la nueva posición del vuelo
                double x = currentPosition.GetX() + distancia * coseno;
                double y = currentPosition.GetY() + distancia * seno;

                currentPosition = new Position(x, y);
            }

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
            Console.WriteLine("Posición PosActual: ({0},{1})", currentPosition.GetX(), currentPosition.GetY());
            Console.WriteLine("******************************");
        }

        public double Distancia(FlightPlan secundario)
        {
            double distancia = this.GetActualPosition().Distancia(secundario.GetActualPosition());
            return distancia;
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
            Position PosActual = this.GetActualPosition();
            Position PosFinal = this.GetFinalPosition();
            double distanciaRestante = PosActual.Distancia(PosFinal);

            if (distanciaRestante == 0 || velocidad <= 0)
            {
                return new Position(PosFinal.GetX(), PosFinal.GetY());
            }

            double distanciaRecorrida = velocidad * minutos / 60;

            if (distanciaRecorrida >= distanciaRestante)
            {
                return new Position(PosFinal.GetX(), PosFinal.GetY());
            }

            double proporcion = distanciaRecorrida / distanciaRestante;

            double x = PosActual.GetX() + (PosFinal.GetX() - PosActual.GetX()) * proporcion;
            double y = PosActual.GetY() + (PosFinal.GetY() - PosActual.GetY()) * proporcion;

            return new Position(x, y);
        }

        // NUEVA FUNCION PARA PREDECIR COLISION ELIPTICA CON DERIVADA
        public ResultadoPrediccionColisiones PredecirConflicto(FlightPlan planB, double diametro)
        {
            Position posA = this.GetActualPosition();
            Position posB = planB.GetActualPosition();

            double dX0 = posA.GetX() - posB.GetX();
            double dY0 = posA.GetY() - posB.GetY();

            double dVx = this.GetVelocidadX() - planB.GetVelocidadX();
            double dVy = this.GetVelocidadY() - planB.GetVelocidadY();

            double denominador = (dVx * dVx) + (dVy * dVy);
            double tMin = 0;

            // Si no van en paralelo, calculamos el tiempo de máxima aproximación
            if (Math.Abs(denominador) > 0.000001)
            {
                tMin = -((dX0 * dVx) + (dY0 * dVy)) / denominador;
                if (tMin < 0)
                {
                    tMin = 0; // Si el punto más cercano fue en el pasado, evaluamos la distancia PosActual (t = 0)
                }
            }

            // Posición relativa en tMin
            double dX_min = dX0 + dVx * tMin;
            double dY_min = dY0 + dVy * tMin;

            // Distancia euclídea real en el punto más cercano
            double distanciaRealMinima = Math.Sqrt((dX_min * dX_min) + (dY_min * dY_min));

            bool hayColision = distanciaRealMinima <= diametro;

            return new ResultadoPrediccionColisiones(tMin, distanciaRealMinima, hayColision);

        }

    }
}