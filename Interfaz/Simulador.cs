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
        int moverParar = 0;

        
        public Simulador()
        { InitializeComponent(); }

        public void DarPlanes(FlightPlanList listaPlanes)
        { this.listaPlanes = listaPlanes; }

        public void DarTiempo(double tiempo)
        { this.tiempo = tiempo; }

        public void DarDistancia(double distancia)
        { this.distancia = distancia; }

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

        private void MensajeColision(FlightPlan a, FlightPlan b)
        {
            MessageBox.Show("Colisión Detectada: " + a.GetId() +
                            " y el vuelo " + b.GetId(),
                            "Error de Colisión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
        }

        private void IdentificarVuelosEnColision()
        {
            int k = 0;
            while (k < listaPlanes.GetNum())
            {
                int l = 0;
                while (l < listaPlanes.GetNum() && k != l)
                {
                    if (listaPlanes.GetFlightPlan(k).HayColisionConElVuelo(listaPlanes.GetFlightPlan(l),
                        Convert.ToInt32(distancia / 2)))
                    {
                        //MensajeColision(listaPlanes.GetFlightPlan(k), listaPlanes.GetFlightPlan(l));
                    }
                    l++;
                }
                k++;
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

            IdentificarVuelosEnColision();
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

                g.DrawLine(PenLinea, Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetInitialPosition().GetX()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetInitialPosition().GetY()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetX()),
                                  Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetFinalPosition().GetY()));

                int j = 0;

                while (j < listaPlanes.GetNum())
                {
                    if (i != j)
                    {
                        if (listaPlanes.GetFlightPlan(i).HayColisionConElVuelo(
                            listaPlanes.GetFlightPlan(j), Convert.ToInt32(distancia / 2)))
                        {
                            g.DrawEllipse(PenCirculoNARANJA, x_centro, y_centro, diametro, diametro);
                        }
                        else
                        {
                            g.DrawEllipse(PenCirculoVERDE, x_centro, y_centro, diametro, diametro);
                        }
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
            if (listaPlanes.CheckColisions(Convert.ToInt32(distancia/2)))
            {
                timer1.Stop(); //Resetea el boton al estado inicial
                auto.Text = "▶ AUTO";
                auto.BackColor = Color.Green;
                moverParar = 0;

                IdentificarVuelosEnColision(); //Muestra los vuelos en colision
            }
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
        }

        private void boton_prediccion_colisiones_Click(object sender, EventArgs e)
        {
            bool haycolision = false;
            int t = 0;
            while(t < 2000 && haycolision == false) // Limite de seguridad de 2000 ciclos para evitar bucles infinitos.
            {
                for (int i = 0; i < listaPlanes.GetNum(); i++)
                {   
                    listaPlanes.GetFlightPlan(i).Mover(tiempo);
                    Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                    listaPuntos.GetDot(i).SetPosition(pos);
                }

                if (listaPlanes.CheckColisions(Convert.ToInt32(distancia / 2)))
                {
                    IdentificarVuelosEnColision();
                    haycolision = true;
                }
                t++;
                Console.WriteLine(t);
            }
            for (int i = 0; i < listaPlanes.GetNum(); i++) //Resetear toda la simulación.
            {   
                listaPlanes.GetFlightPlan(i).Restart();
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
            }

            panelSimulador.Invalidate();
            panelSimulador.Refresh();
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
    }
}
