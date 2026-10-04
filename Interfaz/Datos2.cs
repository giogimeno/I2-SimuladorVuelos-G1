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
    public partial class Datos2 : Form
    {
        double tiempo;
        double distancia;
        
        public Datos2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tiempo = Convert.ToDouble(tiempoCiclo.Text);
                distancia = Convert.ToDouble(distanciaSeguridad.Text);
            }

            catch (FormatException)
            {
                MessageBox.Show("Error en el formato de los datos");
            }
            Close();
        }

        public double DameDistancia()
        {
            return distancia;
        }

        public double DameTiempo()
        {
            return tiempo;
        }

        private void Datos2_Load(object sender, EventArgs e)
        {

        }
    }
}
