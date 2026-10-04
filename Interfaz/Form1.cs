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
            listaPlanes.AddFlightPlan(new FlightPlan("A1", 100, 100, 200, 200, 20));
            listaPlanes.AddFlightPlan(new FlightPlan("A2", 150, 150, 250, 250, 30)); // EVADIR 1rForm (Depuracion)
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
