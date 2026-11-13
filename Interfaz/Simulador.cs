using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Simulador : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        DotList listaPuntos = new DotList();

        Label labelAlarma = new Label();
        double tiempo;
        double distancia;
        int moverParar = 0;

        int pasos = 0; // ciclos de simulación que llevamos

        Pen penCuadricula = new Pen(Color.FromArgb(70, Color.LimeGreen), 1F);
        Pen penAnillo = new Pen(Color.FromArgb(50, Color.LimeGreen), 1F);
        Pen penContorno = new Pen(Color.Black, 1.5F);
        SolidBrush brushMarcas = new SolidBrush(Color.LimeGreen);
        SolidBrush brushTexto = new SolidBrush(Color.White);
        SolidBrush brushFondoTexto = new SolidBrush(Color.FromArgb(160, Color.Black));
        SolidBrush brushAvion = new SolidBrush(Color.Yellow);
        SolidBrush brushSombraVerde = new SolidBrush(Color.FromArgb(50, Color.Green));
        SolidBrush brushSombraNaranja = new SolidBrush(Color.FromArgb(70, Color.Orange));
        Font fuenteMarcas = new Font("Arial", 8);
        Font fuenteEtiqueta = new Font("Arial", 9, FontStyle.Bold);

        // forma del avión, mirando hacia la derecha
        PointF[] formaAvion = { new PointF(14, 0), new PointF(4, -3), new PointF(-2, -14), new PointF(-6, -14),
                                new PointF(-4, -3), new PointF(-9, -2), new PointF(-12, -6), new PointF(-14, -6),
                                new PointF(-12, 0), new PointF(-14, 6), new PointF(-12, 6), new PointF(-9, 2),
                                new PointF(-4, 3), new PointF(-6, 14), new PointF(-2, 14), new PointF(4, 3) };


        public Simulador()
        {
            InitializeComponent();
            panelSimulador.MouseClick += new MouseEventHandler(panelSimulador_MouseClick);

            TemaOscuro oscuro = new TemaOscuro();
            oscuro.AplicarTemaOscuro(this);
            auto.BackColor = Color.Green;
            boton_reset.BackColor = Color.DarkOrange;
            boton_reset.ForeColor = Color.Black;

            labelAlarma.AutoSize = true;
            labelAlarma.BackColor = Color.Red;
            labelAlarma.ForeColor = Color.White;
            labelAlarma.Font = new Font("Arial", 16, FontStyle.Bold);
            labelAlarma.Padding = new Padding(8);
            labelAlarma.Location = new Point(250, 10);
            labelAlarma.Visible = false;
            panelSimulador.Controls.Add(labelAlarma);
        }
        public void DarPlanes(FlightPlanList listaPlanes)
        {
            this.listaPlanes = listaPlanes;
            this.tiempo = listaPlanes.GetTiempo();
            this.distancia = listaPlanes.GetDistanciaSeguridad();
        }
        private void Simulador_Load(object sender, EventArgs e)
        {
            int i = 0;

            while (i < listaPlanes.GetNum())
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);  //guarda el flight plan correspondiente a ese punto en concreto
                Position pos = plan.GetActualPosition();

                Dot dot = new Dot(pos, 10, plan);

                listaPuntos.AddDot(dot);
                i++;
            }
        }
        private void DibujarAvion(Graphics g, float x, float y, float angulo)
        // Dibuja un avión en la posición (x, y), girado "angulo" grados hacia donde vuela
        {
            g.TranslateTransform(x, y);   // movemos el origen al centro del avión
            g.RotateTransform(angulo);    // giramos el dibujo
            g.FillPolygon(brushAvion, formaAvion);
            g.DrawPolygon(penContorno, formaAvion);
            g.ResetTransform();           // volvemos al origen normal
        }
        private void panelSimulador_MouseClick(object sender, MouseEventArgs e)
        // Si se hace clic encima de un avión, enseña su información
        {
            int i = 0;
            bool encontrado = false;
            while (i < listaPlanes.GetNum() && !encontrado)
            {
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                double dx = e.X - pos.GetX();
                double dy = e.Y - pos.GetY();

                if (dx * dx + dy * dy < 15 * 15)   // a menos de 15 píxeles del avión
                {
                    encontrado = true;
                    InformacionAvion form = new InformacionAvion(listaPlanes.GetFlightPlan(i));
                    form.ShowDialog();
                }
                i++;
            }
        }

        private void panelSimulador_Paint(object sender, PaintEventArgs e)
        // Dibuja la cuadrícula de radar, el texto de información y, para cada vuelo, su ruta,
        // su círculo de seguridad con sombreado, el avión y su etiqueta
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int ancho = panelSimulador.Width;
            int alto = panelSimulador.Height;

            g.Clear(Color.FromArgb(5, 20, 15));   // fondo oscuro de pantalla de radar

            // CUADRÍCULA DE RADAR 
            // líneas verticales cada 100
            int xl = 0;
            while (xl < ancho)
            {
                g.DrawLine(penCuadricula, xl, 0, xl, alto);
                if (xl > 0)
                { g.DrawString(xl.ToString(), fuenteMarcas, brushMarcas, xl + 2, 2); }
                xl = xl + 100;
            }
            // líneas horizontales cada 100
            int yl = 0;
            while (yl < alto)
            {
                g.DrawLine(penCuadricula, 0, yl, ancho, yl);
                if (yl > 0)
                { g.DrawString(yl.ToString(), fuenteMarcas, brushMarcas, ancho - 30, yl + 2); }
                yl = yl + 100;
            }
            // anillos de radar centrados en el panel
            int radio = 100;
            while (radio < ancho)
            {
                g.DrawEllipse(penAnillo, ancho / 2 - radio, alto / 2 - radio, 2 * radio, 2 * radio);
                radio = radio + 100;
            }

            // TEXTO CON LA INFORMACIÓN DEL SIMULADOR 
            string control = "CONTROL DE TRÁFICO AÉREO";
            SizeF medida = g.MeasureString(control, fuenteEtiqueta);
            g.DrawString(control, fuenteEtiqueta, brushTexto, 20, alto - medida.Height - 30);

            string info = "Vuelos: " + listaPlanes.GetNum() + "     Tiempo: " + Convert.ToInt32(pasos * tiempo) +
                          " min     Separación mínima: " + Convert.ToInt32(distancia / 2) + " m";
            SizeF medida1 = g.MeasureString(info, fuenteEtiqueta);
            g.FillRectangle(brushFondoTexto, 10, alto - medida1.Height - 16, medida1.Width + 10, medida1.Height + 6);
            g.DrawString(info, fuenteEtiqueta, brushTexto, 15, alto - medida.Height - 13);

            // PARA CADA VUELO 
            Pen PenLinea = new Pen(Color.Red, 0.5F);
            Pen PenCirculoVERDE = new Pen(Color.Green, 1.5F);
            Pen PenCirculoROJO = new Pen(Color.Red, 1.5F);

            double distancia_total = distancia;
            int diametro = Convert.ToInt32(distancia_total / 2);

            int i = 0;
            while (i < listaPlanes.GetNum())
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                int XEsquina = Convert.ToInt32(plan.GetActualPosition().GetX());
                int YEsquina = Convert.ToInt32(plan.GetActualPosition().GetY());

                int x_centro = XEsquina - diametro / 2;    // Ajustar la posición para centrar el círculo
                int y_centro = YEsquina - diametro / 2;

                g.DrawLine(PenLinea, XEsquina, YEsquina,
                                  Convert.ToInt32(plan.GetFinalPosition().GetX()),
                                  Convert.ToInt32(plan.GetFinalPosition().GetY()));


                bool enConflicto = false;
                int j = 0;
                while (j < listaPlanes.GetNum())
                {
                    if (i != j && plan.HayColisionConElVuelo(listaPlanes.GetFlightPlan(j), Convert.ToInt32(distancia / 2)))
                    { enConflicto = true; }
                    j++;
                }


                if (enConflicto)
                {
                    g.FillEllipse(brushSombraNaranja, x_centro, y_centro, diametro, diametro);
                    g.DrawEllipse(PenCirculoROJO, x_centro, y_centro, diametro, diametro);
                }
                else
                {
                    g.FillEllipse(brushSombraVerde, x_centro, y_centro, diametro, diametro);
                    g.DrawEllipse(PenCirculoVERDE, x_centro, y_centro, diametro, diametro);
                }

                // FIGURA AVIÓN
                // el avión mira hacia donde va: ángulo de la recta inicio -> destino
                Position ini = plan.GetInitialPosition();
                Position fin = plan.GetFinalPosition();
                double angulo = Math.Atan2(fin.GetY() - ini.GetY(), fin.GetX() - ini.GetX()) * 180 / Math.PI;
                DibujarAvion(g, XEsquina, YEsquina, Convert.ToSingle(angulo));

                // ETIQUETA CON EL ID Y LA VELOCIDAD 
                string etiqueta = plan.GetId() + "\nV " + plan.GetVelocidad();
                g.DrawString(etiqueta, fuenteEtiqueta, brushTexto, XEsquina + 14, YEsquina + 8);

                i++;
            }
        }
        private void mover_Click(object sender, EventArgs e)
        {
            pasos++;
            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                listaPlanes.GetFlightPlan(i).Mover(tiempo);
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
            }
            panelSimulador.Invalidate();
            panelSimulador.Invalidate();
            ComprobarAlarma();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                listaPlanes.GetFlightPlan(i).Mover(tiempo);
                Position pos = listaPlanes.GetFlightPlan(i).GetActualPosition();
                listaPuntos.GetDot(i).SetPosition(pos);
            }
            panelSimulador.Invalidate();
            panelSimulador.Invalidate();
            ComprobarAlarma();
        }

        private void auto_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;

            if (moverParar == 0)
            {
                timer1.Start();
                auto.Text = "■";
                auto.BackColor = Color.Red;
                moverParar = 1;
            }

            else if (moverParar == 1)
            {
                timer1.Stop();
                auto.Text = "▶ AUTO";
                auto.BackColor = Color.Green;
                moverParar = 0;
            }

        }

        private void MostrarInfoVuelos_Click(object sender, EventArgs e)
        {
            InformaciónDosVuelos form = new InformaciónDosVuelos(listaPlanes);
            form.ShowDialog();
            form.RecibirPlanes();
            if (form.RecibirReset() == 0)
            {
                listaPlanes.Reset();
                listaPuntos.ActualizarPosiciones(listaPlanes);
                panelSimulador.Refresh();
            }
        }

        private void boton_prediccion_colisiones_Click(object sender, EventArgs e)
        {
            double margenSeguridad = distancia / 2.0;
            bool hayConflicto = false;
            int numVuelos = listaPlanes.GetNum();

            for (int i = 0; i < listaPlanes.GetNum(); i++)
            {
                for (int j = i + 1; j < numVuelos; j++)
                {
                    FlightPlan planA = listaPlanes.GetFlightPlan(i);
                    FlightPlan planB = listaPlanes.GetFlightPlan(j);

                    ResultadoPrediccionColisiones res = planA.PredecirConflicto(planB, margenSeguridad);

                    if (res.HayColision)
                    { hayConflicto = true; break; }
                }
                if (hayConflicto) break; // Detiene la búsqueda al encontrar la primera colisión
            }

            if (!hayConflicto)
            {
                MessageBox.Show(
                    "No se han detectado conflictos de trayectorias entre ningún vuelo.",
                    "Sin Conflictos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                GestorDeConflictos gestor = new GestorDeConflictos(listaPlanes);
                gestor.GestionarConflictos();
            }
        }

        private void boton_reset_Click(object sender, EventArgs e)
        {
            listaPlanes.Reset();
            listaPuntos.ActualizarPosiciones(listaPlanes);
            panelSimulador.Refresh();
        }

        private void panelSimulador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }
        private void ComprobarAlarma()
        {
            string parejas = "";
            int i = 0;
            while (i < listaPlanes.GetNum())
            {
                int j = i + 1;
                while (j < listaPlanes.GetNum())
                {
                    if (listaPlanes.GetFlightPlan(i).HayColisionConElVuelo(listaPlanes.GetFlightPlan(j), Convert.ToInt32(distancia / 2)))
                    {
                        parejas = parejas + "   " + listaPlanes.GetFlightPlan(i).GetId() + " - " + listaPlanes.GetFlightPlan(j).GetId();
                    }
                    j++;
                }
                i++;
            }

            if (parejas != "")
            {
                labelAlarma.Text = "¡¡ ALERTA !! COLISIÓN:" + parejas;
                labelAlarma.Visible = true;
            }
            else
            {
                // si ya no hay ninguna pareja en colisón, oculatamos la alerta
                labelAlarma.Visible = false;

            }
        }
    }
}