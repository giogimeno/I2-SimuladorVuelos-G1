using FlightLib;
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
    public partial class Info : Form
    {
        FlightPlan plan;
        public Info()
        {
            InitializeComponent();
        }

        public void DarPlan(FlightPlan plan)
        {
            this.plan = plan;
        }

        private void Info_Load(object sender, EventArgs e)
        {
            textId.Text = plan.GetId();
            textVel.Text = Convert.ToString(plan.GetVelocidad());
            textPosX.Text = Convert.ToString(plan.GetActualPosition().GetX());
            textPosY.Text = Convert.ToString(plan.GetActualPosition().GetY());
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
