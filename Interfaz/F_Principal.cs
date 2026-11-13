using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class Form_Principal : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();

        public Form_Principal()
        { 
            InitializeComponent();
            TemaOscuro oscuro = new TemaOscuro();
            oscuro.AplicarTemaOscuro(this);
            this.Text = "Control de tráfico aéreo";


            Panel panel = new Panel
            {
                BackColor = Color.Black,
                Size = new Size(860, 130),
                Anchor = AnchorStyles.None
            };

            Label titulo = new Label
            {
                Text = "SIMULADOR DE TRÁFICO AÉREO",
                Font = new Font("Arial", 34, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke, 
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            Label subtitulo = new Label
            {
                Text = "Opciones › Añadir datos › Iniciar simulación",
                Font = new Font("Arial", 14),
                ForeColor = Color.LightGreen, 
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            panel.Controls.Add(subtitulo); 
            panel.Controls.Add(titulo);   
            this.Controls.Add(panel);

            // Centrar el panel y mantenerlo centrado al redimensionar
            void Centrar()
            {
                panel.Location = new Point(
                    (this.ClientSize.Width - panel.Width) / 2,
                    (this.ClientSize.Height - panel.Height) / 3);
            }

            Centrar();
            this.Resize += (object s, EventArgs e) => Centrar();


            PictureBox fondo = new PictureBox
            {
                Image = Properties.Resources.GIF_RADAR,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(22, 30, 48)
            };

            this.Controls.Add(fondo);
            fondo.SendToBack(); // al fondo del todo, detrás del menú y del panel
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Identificacion form = new Identificacion();
            form.ShowDialog();
            if (form.GetResult() != 0)
            {
                Close();
            }
        }

        private void añadirDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos_Planes form = new Datos_Planes();
            form.ShowDialog();
            listaPlanes = form.DamePlanes();
        }

        private void añadirTiempoYDistanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos_Tiempo_Distancia form = new Datos_Tiempo_Distancia();
            form.ShowDialog();
            listaPlanes.SetDistanciaSeguridad(form.DameDistancia());
            listaPlanes.SetTiempo(form.DameTiempo());
        }

        private void iniciarSimulaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
            gestor.GestionarConflictos();

            Simulador form = new Simulador();
            form.DarPlanes(listaPlanes);
            form.ShowDialog();
        }

        public void ReiniciarSimulador()
        {
            GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
            gestor.GestionarConflictos();

            Simulador form = new Simulador();
            form.DarPlanes(listaPlanes);
            form.ShowDialog();

        }
    }
}
