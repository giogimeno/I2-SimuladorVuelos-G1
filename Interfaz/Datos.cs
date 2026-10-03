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
    public partial class Datos : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        public Datos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
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

                listaPlanes.AddFlightPlan(plan1);
                listaPlanes.AddFlightPlan(plan2);

            }
            catch (FormatException)
            {
                
            }

            Close();
        }

        public FlightPlanList DamePlanes()
        {
            return listaPlanes;
            
        }
    }
}
