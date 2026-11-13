using FlightLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    // GestorDeConflictos usa la librería FlightLib para gestionar los conflictos de colisión entre aviones.
    // En esta clase NO se realizan cálculos. 
    public class GestorDeConflictos
    {
        private FlightPlanList listaPlanes;

        public GestorDeConflictos(FlightPlanList listaPlanes)
        {
            this.listaPlanes = listaPlanes;
        }

        public void GestionarConflictos()
        {
            if (!listaPlanes.ExisteConflictoConCualquiera())
            {
                return;
            }

            List<string> parejas = listaPlanes.ObtenerParejasEnConflicto();
            string textoParejas = "";
            for (int i = 0; i < parejas.Count && i < 10; i++)
            {
                textoParejas = textoParejas + "\n  - " + parejas[i];
            }
            if (parejas.Count > 10)
            {
                textoParejas = textoParejas + "\n  ... y " + (parejas.Count - 10) + " más";
            }

            DialogResult respuesta = MessageBox.Show(
                "¡Colisión detectada en las trayectorias actuales!\n¿Desea intentar resolver el conflicto automáticamente ajustando velocidades?",
                "Aviso de Conflicto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (respuesta == DialogResult.Yes)
            {
                // Guardamos las velocidades antes de resolver para saber qué vuelos han cambiado
                List<double> velocidadesAntes = new List<double>();
                for (int i = 0; i < listaPlanes.GetNum(); i++)
                {
                    velocidadesAntes.Add(listaPlanes.GetFlightPlan(i).GetVelocidad());
                }

                int conflictosRestantes = listaPlanes.IntentarResolverConflictos();

                // Texto con los vuelos cuya velocidad ha cambiado
                string textoCambios = "";
                for (int i = 0; i < listaPlanes.GetNum(); i++)
                {
                    FlightPlan plan = listaPlanes.GetFlightPlan(i);
                    if (plan.GetVelocidad() != velocidadesAntes[i])
                    {
                        textoCambios = textoCambios + "\n  - Vuelo " + plan.GetId() + ": " +
                                       velocidadesAntes[i].ToString("F2") + " -> " + plan.GetVelocidad().ToString("F2");
                    }
                }

                if (conflictosRestantes == 0)
                {
                    MessageBox.Show(
                        "Todos los conflictos han sido resueltos.\nVelocidades modificadas:" + textoCambios,
                        "Conflictos Resueltos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else if (textoCambios != "")
                {
                    MessageBox.Show(
                        "Se han reducido los conflictos, pero siguen " + conflictosRestantes + " pareja(s) en conflicto.\n" +
                        "Velocidades modificadas:" + textoCambios,
                        "Resuelto parcialmente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
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
