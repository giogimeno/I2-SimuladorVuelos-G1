using FlightLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public class Dot
    {
        PictureBox newpic;
        int size;

        public Dot(Position pos, int size, FlightPlan planvuelo)  //el dot también tiene que recibir el flightplan del avión que representa
        {
            this.size = size;
            double posx = pos.GetX();
            int x = Convert.ToInt32(posx);
            double posy = pos.GetY();
            int y = Convert.ToInt32(posy);

            int x_centro = x - size / 2;    // Ajustar la posición para centrar el punto
            int y_centro = y - size / 2;

            newpic = new PictureBox();
            newpic.Size = new Size(size, size);
            newpic.BackColor = Color.Red;
            newpic.Location = new Point(x_centro, y_centro);

            newpic.Click += (sender, e) =>
            {
                InformacionAvion form = new InformacionAvion(planvuelo);
                form.ShowDialog();
            };
        }

        public void SetPosition(Position pos)
        {
            double posx = pos.GetX();
            int x = Convert.ToInt32(posx);
            double posy = pos.GetY();
            int y = Convert.ToInt32(posy);

            int x_centro = x - this.size / 2;    // Ajustar la posición para centrar el punto
            int y_centro = y - this.size / 2;

            newpic.Location = new Point(x_centro, y_centro);
        }

        public void SetSize(int size)
        {
            newpic.Size = new Size(size, size);
        }

        public PictureBox GetDot()
        {
            return newpic;
        }
    }
}
