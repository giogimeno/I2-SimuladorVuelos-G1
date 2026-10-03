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
            Pen myPen = new Pen(Color.Red);

            int i = 0;
            while (i < listaPlanes.GetNum())
            {
                g.DrawLine(myPen, Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetX()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetActualPosition().GetY()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetX()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetY()));


                i++;
            }
        }
    }
}
