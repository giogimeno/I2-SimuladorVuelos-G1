using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class Form1 : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        double distancia;
        double tiempo;

        public Form1()
        {
            InitializeComponent();
            listaPlanes.AddFlightPlan(new FlightPlan("A1", 100, 100, 300, 300, 50));
            listaPlanes.AddFlightPlan(new FlightPlan("A2", 300, 100, 100, 300, 50)); // EVADIR 1rForm (Depuracion)
            distancia = 100;
            tiempo = 10;
        }

        private void añadirDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos form = new Datos();
            form.ShowDialog();
            listaPlanes = form.DamePlanes();
        }

        private void añadirTiempoYDistanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos2 form = new Datos2();
            form.ShowDialog();
            distancia = form.DameDistancia();
            tiempo = form.DameTiempo();
        }

        private void iniciarSimulaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listaPlanes.CheckColisionesFuturas(Convert.ToDouble(distancia)))
            {
                DialogResult respuesta = MessageBox.Show(
                    "¡Colisión detectada!\n¿Quiere resolver el conflicto?",
                    "Aviso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                if (respuesta == DialogResult.Yes)
                {
                    FlightPlan avionConflicto = null;
                    for (int i = 0; i < listaPlanes.GetNum(); i++)
                    {
                        for (int j = i + 1; j < listaPlanes.GetNum(); j++)
                        {
                            FlightPlan avion1 = listaPlanes.GetFlightPlan(i);
                            FlightPlan avion2 = listaPlanes.GetFlightPlan(j);

                            FlightPlanList prueba = new FlightPlanList();
                            prueba.AddFlightPlan(avion1);
                            prueba.AddFlightPlan(avion2);

                            if (prueba.CheckColisionesFuturas(Convert.ToDouble(distancia)))
                            {
                                double velocidadOriginal = avion1.GetVelocidad();
                                for (int intento = 0; intento < 10; intento++)
                                {
                                    double nuevaVelocidad = avion1.GetVelocidad() * 0.8; // reducir velocidad en un 20%
                                    avion1.SetVelocidad(nuevaVelocidad);
                                    if (!prueba.CheckColisionesFuturas(Convert.ToDouble(distancia)))
                                    {
                                        avionConflicto = avion1;
                                        break;
                                    }
                                }
                                if (avionConflicto == null)
                                {
                                    avion1.SetVelocidad(velocidadOriginal);
                                    continue;
                                }
                                break;
                            }
                        }
                        if (avionConflicto != null)
                        { break; }
                    }

                    if (avionConflicto != null)
                    {
                        MessageBox.Show(
                        $"El conflicto ha sido resuelto.\n" +
                        $"Se ha modificado la velocidad del vuelo {avionConflicto.GetId()} a velocidad {avionConflicto.GetVelocidad()}.",
                        "Conflicto resuelto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se ha encontrado una solución con los ajustes probados. " +
                            "Se mantienen las velocidades originales.",
                            "No se pudo resolver",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                    }
                }
            }

            Simulador form = new Simulador();
            form.DarDistancia(distancia);
            form.DarTiempo(tiempo);
            form.DarPlanes(listaPlanes);
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
