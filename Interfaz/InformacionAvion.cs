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
    public partial class InformacionAvion : Form
    {
        FlightPlan planvuelo;  //creamos un flightplan que recibirá el constructor
        public InformacionAvion(FlightPlan planvuelo) //recibimos un flightplan
        {
            InitializeComponent();

            this.planvuelo = planvuelo;

            //Ahora rellenamos la tabla con la info del flightplan que ha recibido nuestra función

            tablainfo.Rows.Add("Identificador", planvuelo.GetId());
            tablainfo.Rows.Add("Velocidad", planvuelo.GetVelocidad());
            tablainfo.Rows.Add("Posición actual X", planvuelo.GetActualPosition().GetX());
            tablainfo.Rows.Add("Posición actual Y", planvuelo.GetActualPosition().GetY());
            tablainfo.Rows.Add("Posición final X", planvuelo.GetFinalPosition().GetX());
            tablainfo.Rows.Add("Posición final Y", planvuelo.GetFinalPosition().GetY());
            tablainfo.ReadOnly = true;                 // no se puede escribir en las celdas
            tablainfo.AllowUserToAddRows = false;      // quita la fila vacía de abajo
            tablainfo.AllowUserToDeleteRows = false;   // no se pueden borrar filas
            
            TemaOscuro oscuro = new TemaOscuro();
            oscuro.AplicarTemaOscuro(this);
           
        }
    }
}
