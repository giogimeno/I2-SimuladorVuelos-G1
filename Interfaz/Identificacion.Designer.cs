namespace Interfaz
{
    partial class Identificacion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usuarioBox = new System.Windows.Forms.TextBox();
            this.contraseñaBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.registroBtn = new System.Windows.Forms.Button();
            this.inciarSesionBtn = new System.Windows.Forms.Button();
            this.autorrellenoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña:";
            // 
            // usuarioBox
            // 
            this.usuarioBox.Location = new System.Drawing.Point(255, 104);
            this.usuarioBox.Name = "usuarioBox";
            this.usuarioBox.Size = new System.Drawing.Size(257, 26);
            this.usuarioBox.TabIndex = 2;
            // 
            // contraseñaBox
            // 
            this.contraseñaBox.Location = new System.Drawing.Point(276, 168);
            this.contraseñaBox.Name = "contraseñaBox";
            this.contraseñaBox.Size = new System.Drawing.Size(236, 26);
            this.contraseñaBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(120, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "¿Todavía no tienes cuenta?";
            // 
            // registroBtn
            // 
            this.registroBtn.Location = new System.Drawing.Point(367, 333);
            this.registroBtn.Name = "registroBtn";
            this.registroBtn.Size = new System.Drawing.Size(155, 47);
            this.registroBtn.TabIndex = 5;
            this.registroBtn.Text = "Registrarse";
            this.registroBtn.UseVisualStyleBackColor = true;
            this.registroBtn.Click += new System.EventHandler(this.registroBtn_Click);
            // 
            // inciarSesionBtn
            // 
            this.inciarSesionBtn.Location = new System.Drawing.Point(253, 234);
            this.inciarSesionBtn.Name = "inciarSesionBtn";
            this.inciarSesionBtn.Size = new System.Drawing.Size(171, 54);
            this.inciarSesionBtn.TabIndex = 6;
            this.inciarSesionBtn.Text = "Iniciar Sesión";
            this.inciarSesionBtn.UseVisualStyleBackColor = true;
            this.inciarSesionBtn.Click += new System.EventHandler(this.inciarSesionBtn_Click);
            // 
            // autorrellenoBtn
            // 
            this.autorrellenoBtn.Location = new System.Drawing.Point(43, 432);
            this.autorrellenoBtn.Name = "autorrellenoBtn";
            this.autorrellenoBtn.Size = new System.Drawing.Size(162, 41);
            this.autorrellenoBtn.TabIndex = 7;
            this.autorrellenoBtn.Text = "Autorrellenar";
            this.autorrellenoBtn.UseVisualStyleBackColor = true;
            this.autorrellenoBtn.Click += new System.EventHandler(this.autorrellenoBtn_Click);
            // 
            // Identificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 502);
            this.Controls.Add(this.autorrellenoBtn);
            this.Controls.Add(this.inciarSesionBtn);
            this.Controls.Add(this.registroBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.contraseñaBox);
            this.Controls.Add(this.usuarioBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Identificacion";
            this.Text = "Identificacion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usuarioBox;
        private System.Windows.Forms.TextBox contraseñaBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button registroBtn;
        private System.Windows.Forms.Button inciarSesionBtn;
        private System.Windows.Forms.Button autorrellenoBtn;
    }
}