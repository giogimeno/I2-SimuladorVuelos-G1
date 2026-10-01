using FlightLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public class Dot
    {
        PictureBox newpic;

        public Dot(Position pos, int size)
        {
            double posx = pos.GetX();
            int x = Convert.ToInt32(posx);
            double posy = pos.GetY();
            int y = Convert.ToInt32(posy);

            newpic = new PictureBox();
            newpic.Size = new Size(size, size);
            newpic.BackColor = Color.Red;
            newpic.Location = new Point(x, y);
        }

        public void SetPosition(Position pos)
        {
            double posx = pos.GetX();
            int x = Convert.ToInt32(posx);
            double posy = pos.GetY();
            int y = Convert.ToInt32(posy);

            newpic.Location = new Point(x, y);
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
