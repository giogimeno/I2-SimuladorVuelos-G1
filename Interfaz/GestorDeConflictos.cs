using FlightLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    // Creamos una clase GestorDeConflictos para reutilizar codigo para que lo usen todos los formularios.
    public class GestorDeConflictos
    {
        private FlightPlanList listaPlanes;
        private double distancia;

        public GestorDeConflictos(FlightPlanList listaPlanes, double distancia)
        {
            this.listaPlanes = listaPlanes;
            this.distancia = distancia;
        }

        public bool ExisteConflictoConCualquiera(FlightPlan avion, int indiceAvion)
        {
            double margenSeguridad = distancia / 2.0;
            for (int k = 0; k < listaPlanes.GetNum(); k++)
            {
                if (k != indiceAvion)
                {
                    ResultadoPrediccionColisiones res = avion.PredecirConflicto(listaPlanes.GetFlightPlan(k), margenSeguridad);
                    if (res.HayColision)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void GestionarConflictos()
        {
            double margenSeguridad = distancia / 2.0; // Mismo margen de distancia usado en todo el simulador
            bool hayConflictoInicial = false;

            // 1. Comprobar si existe alguna colisión analítica en la lista de listaPlanes
            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                for (int j = i + 1; j < listaPlanes.GetNum(); j++)
                {
                    ResultadoPrediccionColisiones res = listaPlanes.GetFlightPlan(i)
                        .PredecirConflicto(listaPlanes.GetFlightPlan(j), margenSeguridad);

                    if (res.HayColision)
                    {
                        hayConflictoInicial = true;
                        break;
                    }
                }
                if (hayConflictoInicial) break;
            }

            // 2. Si se detectan conflictos, preguntar al usuario si desea resolverlos
            if (hayConflictoInicial)
            {
                DialogResult respuesta = MessageBox.Show(
                    "¡Colisión detectada en las trayectorias actuales!\n¿Desea intentar resolver el conflicto automáticamente ajustando velocidades?",
                    "Aviso de Conflicto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (respuesta == DialogResult.Yes)
                {
                    FlightPlan avionResuelto = null;
                    double velocidadNueva = 0;

                    // Buscar el par en conflicto y probar ajustes de velocidad
                    for (int i = 0; i < listaPlanes.GetNum(); i++)
                    {
                        for (int j = i + 1; j < listaPlanes.GetNum(); j++)
                        {
                            FlightPlan avion1 = listaPlanes.GetFlightPlan(i);
                            FlightPlan avion2 = listaPlanes.GetFlightPlan(j);

                            ResultadoPrediccionColisiones res = avion1.PredecirConflicto(avion2, margenSeguridad);

                            if (res.HayColision)
                            {
                                double velocidadOriginal = avion1.GetVelocidad();
                                bool solucionado = false;

                                // Estrategia A: Intentar REDUCIR velocidad progresivamente (del 95% al 50%)
                                for (double factor = 0.995; factor >= 0.50; factor -= 0.005)
                                {
                                    double pruebaVelocidad = velocidadOriginal * factor;
                                    avion1.SetVelocidad(pruebaVelocidad);

                                    // Comprobar que no colisiona con NINGÚN avión del espacio aéreo
                                    if (!ExisteConflictoConCualquiera(avion1, i))
                                    {
                                        avionResuelto = avion1;
                                        velocidadNueva = pruebaVelocidad;
                                        solucionado = true;
                                        break;
                                    }
                                }

                                // Estrategia B: Si reducir no funciona, intentar AUMENTAR velocidad (del 105% al 150%)
                                if (solucionado == false)
                                {
                                    for (double factor = 1.005; factor <= 1.50; factor += 0.005)
                                    {
                                        double pruebaVelocidad = velocidadOriginal * factor;
                                        avion1.SetVelocidad(pruebaVelocidad);

                                        if (!ExisteConflictoConCualquiera(avion1, i))
                                        {
                                            avionResuelto = avion1;
                                            velocidadNueva = pruebaVelocidad;
                                            solucionado = true;
                                            break;
                                        }
                                    }
                                }

                                // Si no hubo solución para este avión, restaurar su velocidad inicial
                                if (solucionado == false)
                                { avion1.SetVelocidad(velocidadOriginal); }
                                else
                                { break; }
                            }
                        }
                        if (avionResuelto != null) break;
                    }

                    if (avionResuelto != null)
                    {
                        MessageBox.Show(
                            $"El conflicto ha sido resuelto.\n" +
                            $"Se ha modificado la velocidad del vuelo {avionResuelto.GetId()} a {velocidadNueva:F2} unidades.",
                            "Conflicto Resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se ha encontrado una solución de velocidad dentro de los márgenes seguros (50% - 150%).\n" +
                            "Se mantienen las velocidades originales.",
                            "No se pudo resolver",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }
        }
    }
}
