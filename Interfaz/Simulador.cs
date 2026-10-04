using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Simulador : Form
    {
        FlightPlanList listaPlanes;
        DotList listaPuntos = new DotList();
        double tiempo;
        double distancia;

        
        public Simulador()
        {
            InitializeComponent();
        }

        public void DarPlanes(FlightPlanList listaPlanes)
        {
            this.listaPlanes = listaPlanes;
        }

        public void DarTiempo(double tiempo)
        {
            this.tiempo = tiempo;
        }

        public void DarDistancia(double distancia)
        {
            this.distancia = distancia;
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
            Pen PenLinea = new Pen(Color.Red);
            Pen PenCirculo = new Pen(Color.Yellow);

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

                g.DrawEllipse(PenCirculo, x_centro, y_centro, diametro, diametro);
                i++;
            }
        }

        private void panelSimulador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
