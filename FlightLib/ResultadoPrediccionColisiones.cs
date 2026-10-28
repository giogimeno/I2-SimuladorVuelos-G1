using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class ResultadoPrediccionColisiones
    {
        public bool HayColision { get; set; }
        public double Tiempo { get; set; }          // Instante t de máxima aproximación (segundos)
        public double DistanciaMinima { get; set; } // Distancia física mínima a la que se cruzan (píxeles)

        public ResultadoPrediccionColisiones(double tiempo, double distanciaMinima, bool hayColision)
        {
            this.Tiempo = tiempo;
            this.DistanciaMinima = distanciaMinima;
            this.HayColision = hayColision;
        }
        public double GetTiempo()
        { return Tiempo; }

        public double GetDistanciaMinima()
        { return DistanciaMinima; }

        public bool GetHayColision()
        { return HayColision; } 

        private void SetTiempo(double tiempo)
        { this.Tiempo = tiempo; }

        private void SetDistanciaMinima(double distancia)
        { this.DistanciaMinima = distancia; }

        private void SetHayColision(bool hayColision)
        { this.HayColision = hayColision; }
    }
}
