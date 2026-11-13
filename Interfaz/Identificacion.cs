using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestiónRegistro;

namespace Interfaz
{
    public partial class Identificacion : Form
    {
        Gestion mi_base = new Gestion();
        int result = -1;
        public Identificacion()
        {
            InitializeComponent();
            mi_base.Iniciar();
        }

        private void inciarSesionBtn_Click(object sender, EventArgs e)
        {
            string username = usuarioBox.Text;
            string password = contraseñaBox.Text;

            int resultado = mi_base.EncontrarCuenta(username, password);

            if (resultado == 0)
            {
                mi_base.Cerrar();
                result = 0;
                Close();
            }

            else
            {
                MessageBox.Show("Usuario o contraseña incorrectas");
            }
        }

        private void registroBtn_Click(object sender, EventArgs e)
        {
            RegistroForm registro = new RegistroForm();
            registro.ShowDialog();
        }

        private void autorrellenoBtn_Click(object sender, EventArgs e)
        {
            usuarioBox.Text = "claudia.allende";
            contraseñaBox.Text = "claudiaisthebest";
        }

        public int GetResult()
        {
            return result;
        }
    }
}
