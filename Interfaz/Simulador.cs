using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Simulador : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        DotList listaPuntos = new DotList();
        double tiempo;
        double distancia;
        int moverParar = 0;

        public Simulador()
        { InitializeComponent(); }

        public void DarPlanes(FlightPlanList listaPlanes)
        { 
            this.listaPlanes = listaPlanes;
            this.tiempo = listaPlanes.GetTiempo();
            this.distancia = listaPlanes.GetDistanciaSeguridad();
        }
        private void Simulador_Load(object sender, EventArgs e)
        {
            int i = 0;

            while (i < listaPlanes.GetNum())
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);  //guarda el flight plan correspondiente a ese punto en concreto
                Position pos = plan.GetActualPosition();

                Dot dot = new Dot(pos, 10, plan);

                listaPuntos.AddDot(dot);
                panelSimulador.Controls.Add(dot.GetDot());

                i++;
            }
        }

        private void mover_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                listaPlanes.GetFlightPlan(i).Mover(tiempo);
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
            }
            panelSimulador.Invalidate();
        }

        private void panelSimulador_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen PenLinea = new Pen(Color.Red, 0.5F);
            Pen PenCirculoVERDE = new Pen(Color.Green, 1.5F);
            Pen PenCirculoNARANJA = new Pen(Color.Orange, 1.5F);

            double distancia_total = distancia;
            int diametro = Convert.ToInt32(distancia_total / 2);

            int i = 0;
            while (i < listaPlanes.GetNum())
            {
                int XEsquina = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetX());
                int YEsquina = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetY());

                int x_centro = XEsquina - diametro / 2;    // Ajustar la posición para centrar el punto
                int y_centro = YEsquina - diametro / 2;

                g.DrawLine(PenLinea, Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetX()),
                              Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetY()),
                              Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetX()),
                              Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetY()));

                int j = 0;
                while (j < listaPlanes.GetNum())
                {
                    if (i != j)
                    {
                        FlightPlan plani = listaPlanes.GetFlightPlan(i);
                        FlightPlan planj = listaPlanes.GetFlightPlan(j);

                        if (plani.Distancia(planj) <= diametro)
                        { g.DrawEllipse(PenCirculoNARANJA, x_centro, y_centro, diametro, diametro); }
                        else
                        { g.DrawEllipse(PenCirculoVERDE, x_centro, y_centro, diametro, diametro); }
                    }
                    j++;
                }
                i++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                listaPlanes.GetFlightPlan(i).Mover(tiempo);
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
            }
            panelSimulador.Invalidate();
        }

        private void auto_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;

            if (moverParar == 0)
            {
                timer1.Start();
                auto.Text = "■";
                auto.BackColor = Color.Red;
                moverParar = 1;
            }

            else if (moverParar == 1)
            {
                timer1.Stop();
                auto.Text = "▶ AUTO";
                auto.BackColor = Color.Green;
                moverParar = 0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InformaciónDosVuelos form = new InformaciónDosVuelos(listaPlanes);
            form.ShowDialog();
            form.RecibirPlanes();
            if (form.RecibirReset() == 0)
            {
                for (int i = 0; i < listaPlanes.GetNum(); i++)
                {
                    listaPlanes.GetFlightPlan(i).Restart();
                    Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                    listaPuntos.GetDot(i).SetPosition(pos);
                    panelSimulador.Invalidate();
                    panelSimulador.Refresh();
                }

            }

        }

        private void boton_prediccion_colisiones_Click(object sender, EventArgs e)
        {
            double margenSeguridad = distancia / 2.0;
            bool hayConflicto = false;
            int numVuelos = listaPlanes.GetNum();

            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                for (int j = i + 1; j < numVuelos; j++)
                {
                    FlightPlan planA = listaPlanes.GetFlightPlan(i);
                    FlightPlan planB = listaPlanes.GetFlightPlan(j);

                    ResultadoPrediccionColisiones res = planA.PredecirConflicto(planB, margenSeguridad);

                    if (res.HayColision)
                    { hayConflicto = true; break; }
                }
                if (hayConflicto) break; // Detiene la búsqueda al encontrar la primera colisión
            }

            if (!hayConflicto)
            {
                MessageBox.Show(
                    "No se han detectado conflictos de trayectorias entre ningún vuelo.",
                    "Sin Conflictos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
                gestor.GestionarConflictos();
            }
        }

        private void boton_reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPlanes.GetNum(); i++) //Resetear toda la simulación.
            {
                listaPlanes.GetFlightPlan(i).Restart();
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
                panelSimulador.Invalidate();
                panelSimulador.Refresh();
            }
        }

        private void panelSimulador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }

        private void guardarPlanesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog selector = new SaveFileDialog();
            selector.Filter = "Archivos de texto (*.txt)|*.txt";

            int i = 1;

            if (selector.ShowDialog() == DialogResult.OK)
            {

                i = listaPlanes.GuardarPlanes(selector.FileName);

            }

            if (i == 0)
            {
                MessageBox.Show("Planes guardados correctamente.");
            }
        }

        private void cargarPlanesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog selector = new OpenFileDialog();
            selector.Filter = "Archivos de texto (*.txt)|*.txt";

            int i = 1;

            if (selector.ShowDialog() == DialogResult.OK)
            {

                i = listaPlanes.CargarArchivo(selector.FileName);

            }

            if (i == 0)
            {
                MessageBox.Show("Planes cargados correctamente. Vuelva a abrir el simulador.");
                Close();
                
            }

            else if (i == -1)
            {
                MessageBox.Show("Error con el formato del archivo");
            }

            else if (i == -2)
            {
                MessageBox.Show("Archivo no encotrado.");
            }
        }
    }
}
