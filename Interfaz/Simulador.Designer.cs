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
            this.mover = new System.Windows.Forms.Button();
            this.panelSimulador = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).BeginInit();
            this.SuspendLayout();
            // 
            // mover
            // 
            this.mover.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mover.Location = new System.Drawing.Point(457, 22);
            this.mover.Name = "mover";
            this.mover.Size = new System.Drawing.Size(111, 33);
            this.mover.TabIndex = 5;
            this.mover.Text = "MOVER";
            this.mover.UseVisualStyleBackColor = true;
            this.mover.Click += new System.EventHandler(this.mover_Click);
            // 
            // panelSimulador
            // 
            this.panelSimulador.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelSimulador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.panelSimulador.Location = new System.Drawing.Point(12, 61);
            this.panelSimulador.Name = "panelSimulador";
            this.panelSimulador.RowHeadersWidth = 51;
            this.panelSimulador.RowTemplate.Height = 24;
            this.panelSimulador.Size = new System.Drawing.Size(1050, 700);
            this.panelSimulador.TabIndex = 4;
            this.panelSimulador.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.panelSimulador_CellContentClick);
            this.panelSimulador.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSimulador_Paint);
            // 
            // Simulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 789);
            this.Controls.Add(this.mover);
            this.Controls.Add(this.panelSimulador);
            this.Name = "Simulador";
            this.Text = "Simulador";
            this.Load += new System.EventHandler(this.Simulador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mover;
        private System.Windows.Forms.DataGridView panelSimulador;
    }
}