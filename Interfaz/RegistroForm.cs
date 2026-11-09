using GestiónRegistro;
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
    public partial class RegistroForm : Form
    {
        Gestion mi_base = new Gestion();
        public RegistroForm()
        {
            InitializeComponent();
            mi_base.Iniciar();
        }

        private void registrarseBtn_Click(object sender, EventArgs e)
        {
            string username = usuarioBox.Text;
            string password = contraseñaBox.Text;

            int resultado1 = mi_base.EncontrarCuenta(username, password);
            int resultado2 = mi_base.EncontrarUsername(username);

            if (resultado1 == 0)
            {
                MessageBox.Show("Esta cuenta ya existe, pruebe de iniciar sesión");
                this.Close();
            }

            else
            {
                if (resultado2 == 0)
                {
                    MessageBox.Show("Ese nombre de usuario ya existe, por favor, introduzca uno diferente");
                }

                else
                {
                    int resultado3 = mi_base.AddUsuario(username, password);
                    if (resultado3 == 0)
                    {
                        MessageBox.Show("Usuario añadido correctamente, pruebe de inciar sesión");
                        mi_base.Cerrar();
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("El usuario no se ha podido añadir");
                    }


                }
            }
        }

        private void cancelarBtn_Click(object sender, EventArgs e)
        {
            mi_base.Cerrar();
            this.Close();
        }
    }
}
