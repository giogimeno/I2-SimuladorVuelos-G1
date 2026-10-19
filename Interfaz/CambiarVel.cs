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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                newVelocidad = Convert.ToDouble(velocidadTextBox.Text);
                Close();
                reset = 0;

            }

            catch (FormatException)
            {
                MessageBox.Show("Error al introducir los datos.");
                reset = -1;
            }


        }

        public double RecibirVelocidad()
        {
            return newVelocidad;
        }

        public int RecibirReset()
        {
            return reset;
        }
    }
}
