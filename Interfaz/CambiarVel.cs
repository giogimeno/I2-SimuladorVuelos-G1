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
    public partial class CambiarVel : Form
    {
        double newVelocidad;
        int reset = -1;
        public CambiarVel()
        {
            InitializeComponent();
        }


        public double RecibirVelocidad()
        {
            try
            {
                newVelocidad = Convert.ToDouble(velocidadTextBox.Text);
                Close();
            }

            catch (FormatException)
            {
                MessageBox.Show("Error al introducir los datos.");
            }

            reset = 0;
            return newVelocidad;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.newVelocidad = Convert.ToDouble(velocidadTextBox.Text);
                reset = 0;
                Close();
            }

            catch (FormatException)
            {
                MessageBox.Show("Error al introducir los datos.");
                reset = -1;
            }
        }
        private void CambiarVel_Load(object sender, EventArgs e)
        {

        }
        public int RecibirReset()
        { return reset; }
    }
}
