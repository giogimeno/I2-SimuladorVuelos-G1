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
    public partial class Datos_Planes : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        public Datos_Planes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = plan1Id.Text;
            double x1 = Convert.ToDouble(plan1XInicio.Text);
            double y1 = Convert.ToDouble(plan1YInicio.Text);
            double x2 = Convert.ToDouble(plan1XFinal.Text);
            double y2 = Convert.ToDouble(plan1YFinal.Text);
            double v = Convert.ToDouble(plan1Vel.Text);

            FlightPlan plan1 = new FlightPlan(id, x1, y1, x2, y2, v);

            id = plan2Id.Text;
            x1 = Convert.ToDouble(plan2XInicio.Text);
            y1 = Convert.ToDouble(plan2YInicio.Text);
            x2 = Convert.ToDouble(plan2XFinal.Text);
            y2 = Convert.ToDouble(plan2YFinal.Text);
            v = Convert.ToDouble(plan2Vel.Text);

            FlightPlan plan2 = new FlightPlan(id, x1, y1, x2, y2, v);

            if (plan1.GetActualPosition().GetX() == plan1.GetFinalPosition().GetX() && plan1.GetActualPosition().GetY() == plan1.GetFinalPosition().GetY())
            {
                MessageBox.Show(
                "El plan de vuelo 1 tiene la misma posición inicial y final.\nPor favor, verifique la información e inténtelo de nuevo.",
                "Atención - Error de Datos_Planes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
                //Close();
                return;
            }
            else if (plan2.GetActualPosition().GetX() == plan2.GetFinalPosition().GetX() && plan2.GetActualPosition().GetY() == plan2.GetFinalPosition().GetY())
            {
                MessageBox.Show(
                "El plan de vuelo 2 tiene la misma posición inicial y final.\nPor favor, verifique la información e inténtelo de nuevo.",
                "Atención - Error de Datos_Planes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
                return;
            }
            else
            {
                listaPlanes.AddFlightPlan(plan1);
                listaPlanes.AddFlightPlan(plan2);
            }
            Close();
        }

        public FlightPlanList DamePlanes()
        {
            return listaPlanes;
            
        }

        private void autoRelleno_Click(object sender, EventArgs e)
        {
            plan1Id.Text = "A1";
            plan1XInicio.Text = "100";
            plan1YInicio.Text = "100";
            plan1XFinal.Text = "300";
            plan1YFinal.Text = "300";
            plan1Vel.Text = "50";

            plan2Id.Text = "A2";
            plan2XInicio.Text = "300";
            plan2YInicio.Text = "100";
            plan2XFinal.Text = "100";
            plan2YFinal.Text = "300";
            plan2Vel.Text = "50";
        }

        private void Datos_Load(object sender, EventArgs e)
        {

        }
    }
}
