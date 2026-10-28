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
    public partial class Datos_Tiempo_Distancia : Form
    {
        double tiempo;
        double distancia;
        
        public Datos_Tiempo_Distancia()
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
                MessageBox.Show(
                "Los datos introducidos no son válidos o están incompletos.\nPor favor, verifique la información e inténtelo de nuevo.",
                "Atención - Error de Datos_Planes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
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

        private void autoRellenar2_Click(object sender, EventArgs e)
        {
            distanciaSeguridad.Text = "100";
            tiempoCiclo.Text = "10";
        }
    }
}
