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
            this.MostrarInfoVuelos = new System.Windows.Forms.Button();
            this.boton_prediccion_colisiones = new System.Windows.Forms.Button();
            this.boton_reset = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarPlanesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarPlanesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mover
            // 
            this.mover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mover.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mover.Location = new System.Drawing.Point(752, 30);
            this.mover.Name = "mover";
            this.mover.Size = new System.Drawing.Size(145, 31);
            this.mover.TabIndex = 5;
            this.mover.Text = "AVANZAR";
            this.mover.UseVisualStyleBackColor = false;
            this.mover.Click += new System.EventHandler(this.mover_Click);
            // 
            // panelSimulador
            // 
            this.panelSimulador.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelSimulador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.panelSimulador.Location = new System.Drawing.Point(12, 68);
            this.panelSimulador.Name = "panelSimulador";
            this.panelSimulador.RowHeadersWidth = 51;
            this.panelSimulador.RowTemplate.Height = 24;
            this.panelSimulador.Size = new System.Drawing.Size(1155, 709);
            this.panelSimulador.TabIndex = 4;
            this.panelSimulador.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.panelSimulador_CellContentClick);
            this.panelSimulador.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSimulador_Paint);
            // 
            // auto
            // 
            this.auto.BackColor = System.Drawing.Color.Green;
            this.auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auto.ForeColor = System.Drawing.SystemColors.Control;
            this.auto.Location = new System.Drawing.Point(1034, 29);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(133, 31);
            this.auto.TabIndex = 6;
            this.auto.Text = "▶ AUTO";
            this.auto.UseVisualStyleBackColor = false;
            this.auto.Click += new System.EventHandler(this.auto_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MostrarInfoVuelos
            // 
            this.MostrarInfoVuelos.Location = new System.Drawing.Point(17, 30);
            this.MostrarInfoVuelos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MostrarInfoVuelos.Name = "MostrarInfoVuelos";
            this.MostrarInfoVuelos.Size = new System.Drawing.Size(237, 33);
            this.MostrarInfoVuelos.TabIndex = 7;
            this.MostrarInfoVuelos.Text = "Mostrar información de los vuelos";
            this.MostrarInfoVuelos.UseVisualStyleBackColor = false;
            this.MostrarInfoVuelos.Click += new System.EventHandler(this.MostrarInfoVuelos_Click);
            // 
            // boton_prediccion_colisiones
            // 
            this.boton_prediccion_colisiones.Location = new System.Drawing.Point(260, 30);
            this.boton_prediccion_colisiones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_prediccion_colisiones.Name = "boton_prediccion_colisiones";
            this.boton_prediccion_colisiones.Size = new System.Drawing.Size(185, 33);
            this.boton_prediccion_colisiones.TabIndex = 8;
            this.boton_prediccion_colisiones.Text = "Predicción de Colisiones";
            this.boton_prediccion_colisiones.UseVisualStyleBackColor = true;
            this.boton_prediccion_colisiones.Click += new System.EventHandler(this.boton_prediccion_colisiones_Click);
            // 
            // boton_reset
            // 
            this.boton_reset.BackColor = System.Drawing.Color.DarkOrange;
            this.boton_reset.Cursor = System.Windows.Forms.Cursors.Default;
            this.boton_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boton_reset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.boton_reset.Location = new System.Drawing.Point(903, 29);
            this.boton_reset.Name = "boton_reset";
            this.boton_reset.Size = new System.Drawing.Size(125, 31);
            this.boton_reset.TabIndex = 9;
            this.boton_reset.Text = "RESET";
            this.boton_reset.UseVisualStyleBackColor = false;
            this.boton_reset.Click += new System.EventHandler(this.boton_reset_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1192, 28);
            this.menuStrip2.TabIndex = 11;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guardarPlanesToolStripMenuItem,
            this.cargarPlanesToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // guardarPlanesToolStripMenuItem
            // 
            this.guardarPlanesToolStripMenuItem.Name = "guardarPlanesToolStripMenuItem";
            this.guardarPlanesToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.guardarPlanesToolStripMenuItem.Text = "Guardar planes";
            // 
            // cargarPlanesToolStripMenuItem
            // 
            this.cargarPlanesToolStripMenuItem.Name = "cargarPlanesToolStripMenuItem";
            this.cargarPlanesToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.cargarPlanesToolStripMenuItem.Text = "Cargar Planes";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(576, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 32);
            this.button2.TabIndex = 12;
            this.button2.Text = "RETROCEDER";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // Simulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 789);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.boton_reset);
            this.Controls.Add(this.boton_prediccion_colisiones);
            this.Controls.Add(this.MostrarInfoVuelos);
            this.Controls.Add(this.auto);
            this.Controls.Add(this.mover);
            this.Controls.Add(this.panelSimulador);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Simulador";
            this.Text = "Simulador";
            this.Load += new System.EventHandler(this.Simulador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelSimulador)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mover;
        private System.Windows.Forms.DataGridView panelSimulador;
        private System.Windows.Forms.Button auto;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button MostrarInfoVuelos;
        private System.Windows.Forms.Button boton_prediccion_colisiones;
        private System.Windows.Forms.Button boton_reset;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarPlanesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargarPlanesToolStripMenuItem;
        private System.Windows.Forms.Button button2;
    }
}