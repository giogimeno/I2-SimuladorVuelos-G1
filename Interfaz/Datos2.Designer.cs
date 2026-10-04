namespace Interfaz
{
    partial class Datos2
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tiempoCiclo = new System.Windows.Forms.TextBox();
            this.distanciaSeguridad = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tiempo de Ciclo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Distancia Seguridad";
            // 
            // tiempoCiclo
            // 
            this.tiempoCiclo.Location = new System.Drawing.Point(236, 39);
            this.tiempoCiclo.Name = "tiempoCiclo";
            this.tiempoCiclo.Size = new System.Drawing.Size(51, 22);
            this.tiempoCiclo.TabIndex = 6;
            // 
            // distanciaSeguridad
            // 
            this.distanciaSeguridad.Location = new System.Drawing.Point(236, 88);
            this.distanciaSeguridad.Name = "distanciaSeguridad";
            this.distanciaSeguridad.Size = new System.Drawing.Size(51, 22);
            this.distanciaSeguridad.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(321, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 35);
            this.button1.TabIndex = 24;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Datos2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 153);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.distanciaSeguridad);
            this.Controls.Add(this.tiempoCiclo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "Datos2";
            this.Text = "Datos2";
            this.Load += new System.EventHandler(this.Datos2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tiempoCiclo;
        private System.Windows.Forms.TextBox distanciaSeguridad;
        private System.Windows.Forms.Button button1;
    }
}