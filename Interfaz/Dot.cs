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
        int x;
        int y;
        int size;

        public Dot(Position pos, int size)
        {
            double posx = pos.GetX();
            int posx_rec = Convert.ToInt32(posx);
            double posy = pos.GetX();
            int posy_rec = Convert.ToInt32(posy);

            this.x = posx_rec;
            this.y = posy_rec;
            this.size = size;

            PictureBox newpic = new PictureBox();

            newpic.Size = new Size(size, size);
            newpic.BackColor = Color.Red;
            newpic.Location = new Point(this.x, this.y);            
        }

        public void SetPosition(int x, int y)
        { this.x = x; this.y = y; }

        public void SetSize(int size)
        { this.size = size; }
    }
}
