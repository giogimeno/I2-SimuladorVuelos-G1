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
    public partial class Form_Principal : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();

        public Form_Principal()
        { 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Identificacion form = new Identificacion();
            form.ShowDialog();
            if (form.GetResult() != 0)
            {
                Close();
            }
        }

        private void añadirDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos_Planes form = new Datos_Planes();
            form.ShowDialog();
            listaPlanes = form.DamePlanes();
        }

        private void añadirTiempoYDistanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos_Tiempo_Distancia form = new Datos_Tiempo_Distancia();
            form.ShowDialog();
            listaPlanes.SetDistanciaSeguridad(form.DameDistancia());
            listaPlanes.SetTiempo(form.DameTiempo());
        }

        private void iniciarSimulaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
            gestor.GestionarConflictos();

            Simulador form = new Simulador();
            form.DarPlanes(listaPlanes);
            form.ShowDialog();
        }

        public void ReiniciarSimulador()
        {
            GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
            gestor.GestionarConflictos();

            Simulador form = new Simulador();
            form.DarPlanes(listaPlanes);
            form.ShowDialog();

        }
    }
}
