using System;
using System.Drawing;
using System.Windows.Forms;


namespace Interfaz
{
    public class TemaOscuro
    {
        Color fondo = Color.FromArgb(20, 25, 35);
        Color fondoControl = Color.FromArgb(40, 50, 70);
        Color texto = Color.White;

        public void AplicarTemaOscuro(Form form)
        {
            form.BackColor = fondo;
            form.ForeColor = texto;
            foreach (Control control in form.Controls)
            {
                if (control is Button || control is Label || control is TextBox || control is DataGridView)
                {
                    control.BackColor = fondoControl;
                    control.ForeColor = texto;
                }
            }
        }
        private void PonerTablaOscura(DataGridView t)
        // Colores oscuros para una tabla (celdas, cabecera y fondo)
        {
            t.EnableHeadersVisualStyles = false;
            t.BackgroundColor = fondo;
            t.GridColor = fondoControl;
            t.DefaultCellStyle.BackColor = fondo;
            t.DefaultCellStyle.ForeColor = texto;
            t.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 90, 140);
            t.DefaultCellStyle.SelectionForeColor = texto;
            t.ColumnHeadersDefaultCellStyle.BackColor = fondoControl;
            t.ColumnHeadersDefaultCellStyle.ForeColor = texto;
            t.RowHeadersDefaultCellStyle.BackColor = fondoControl;
        }
    }
}

