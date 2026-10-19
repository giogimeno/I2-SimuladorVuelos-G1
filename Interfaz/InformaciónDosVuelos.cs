using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class InformaciónDosVuelos : Form
    {
        FlightPlanList planes;
        int reset;
        public InformaciónDosVuelos(FlightPlanList planes)
        {
            InitializeComponent();

            this.planes = planes;

            int i = 0;

            while (i < planes.GetNum())
            {
                vuelostabla.Rows.Add();

                vuelostabla.Rows[i].Cells[0].Value = planes.GetFlightPlan(i).GetId();
                vuelostabla.Rows[i].Cells[1].Value = Convert.ToInt32(planes.GetFlightPlan(i).GetActualPosition().GetX());
                vuelostabla.Rows[i].Cells[2].Value = Convert.ToInt32(planes.GetFlightPlan(i).GetActualPosition().GetY());
                vuelostabla.Rows[i].Cells[3].Value = planes.GetFlightPlan(i).GetFinalPosition().GetX();
                vuelostabla.Rows[i].Cells[4].Value = planes.GetFlightPlan(i).GetFinalPosition().GetY();
                vuelostabla.Rows[i].Cells[5].Value = planes.GetFlightPlan(i).GetVelocidad();

                i = i + 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void vuelostabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 5)
            {
                int i = e.RowIndex;
                FlightPlan fp1 = planes.GetFlightPlan(i);
                FlightPlan fp2;

                if (i == 0)
                {
                    fp2 = planes.GetFlightPlan(1);
                }

                else
                {
                    fp2 = planes.GetFlightPlan(0);
                }

                double distancia = fp1.Distancia(fp2);

                MostrarDistancia formulario = new MostrarDistancia(distancia);
                formulario.ShowDialog();
            }

            else
            {
                int i = e.RowIndex;
                CambiarVel form = new CambiarVel();
                form.ShowDialog();
                planes.GetFlightPlan(i).SetVelocidad(form.RecibirVelocidad());
                reset = form.RecibirReset();
                if (reset == 0)
                {
                    vuelostabla.Rows[i].Cells[5].Value = form.RecibirVelocidad();
                }

            }

        }

        public FlightPlanList RecibirPlanes()
        {
            return planes;
        }

        public int RecibirReset()
        {
            return reset;
        }
    }
}
