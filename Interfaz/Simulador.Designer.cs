namespace Interfaz
{
    partial class Simulador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mover = new System.Windows.Forms.Button();
            this.panelSimulador = new System.Windows.Forms.DataGridView();
            this.auto = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).BeginInit();
            this.SuspendLayout();
            // 
            // mover
            // 
            this.mover.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mover.Location = new System.Drawing.Point(514, 28);
            this.mover.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mover.Name = "mover";
            this.mover.Size = new System.Drawing.Size(125, 41);
            this.mover.TabIndex = 5;
            this.mover.Text = "MOVER";
            this.mover.UseVisualStyleBackColor = true;
            this.mover.Click += new System.EventHandler(this.mover_Click);
            // 
            // panelSimulador
            // 
            this.panelSimulador.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelSimulador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.panelSimulador.Location = new System.Drawing.Point(14, 76);
            this.panelSimulador.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSimulador.Name = "panelSimulador";
            this.panelSimulador.RowHeadersWidth = 51;
            this.panelSimulador.RowTemplate.Height = 24;
            this.panelSimulador.Size = new System.Drawing.Size(1181, 875);
            this.panelSimulador.TabIndex = 4;
            this.panelSimulador.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSimulador_Paint);
            // 
            // auto
            // 
            this.auto.BackColor = System.Drawing.Color.Green;
            this.auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auto.ForeColor = System.Drawing.SystemColors.Control;
            this.auto.Location = new System.Drawing.Point(646, 28);
            this.auto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(125, 41);
            this.auto.TabIndex = 6;
            this.auto.Text = "▶ AUTO";
            this.auto.UseVisualStyleBackColor = false;
            this.auto.Click += new System.EventHandler(this.auto_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(272, 41);
            this.button1.TabIndex = 7;
            this.button1.Text = "Mostrar información de los vuelos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Simulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 986);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.auto);
            this.Controls.Add(this.mover);
            this.Controls.Add(this.panelSimulador);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Simulador";
            this.Text = "Simulador";
            this.Load += new System.EventHandler(this.Simulador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mover;
        private System.Windows.Forms.DataGridView panelSimulador;
        private System.Windows.Forms.Button auto;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}