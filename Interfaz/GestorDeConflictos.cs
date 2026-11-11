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

            DialogResult respuesta = MessageBox.Show(
                "¡Colisión detectada en las trayectorias actuales!\n¿Desea intentar resolver el conflicto automáticamente ajustando velocidades?",
                "Aviso de Conflicto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (respuesta == DialogResult.Yes)
            {
                FlightPlan avionResuelto = listaPlanes.IntentarResolverConflictos();

                // Si es distinto de null, significa que está resuelto
                if (avionResuelto != null)
                {
                    MessageBox.Show(
                        $"El conflicto ha sido resuelto.\n" +
                        $"Se ha modificado la velocidad del vuelo {avionResuelto.GetId()} a {avionResuelto.GetVelocidad():F2} unidades.",
                        "Conflicto Resuelto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return; // Conflicto resuelto
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
                    return; // No se pudo resolver el conflicto
                }
            }

            return; // Usuario decidió no resolver el conflicto
        }
    }
}
