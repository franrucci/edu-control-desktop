namespace Vista
{
    partial class FormAuditoria
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
            gBoxAuditoria = new GroupBox();
            btnAuditoriaLoginLogout = new Button();
            btnAuditoriaAlumnos = new Button();
            btnVolver = new Button();
            gBoxAuditoria.SuspendLayout();
            SuspendLayout();
            // 
            // gBoxAuditoria
            // 
            gBoxAuditoria.Controls.Add(btnAuditoriaLoginLogout);
            gBoxAuditoria.Controls.Add(btnAuditoriaAlumnos);
            gBoxAuditoria.Location = new Point(12, 12);
            gBoxAuditoria.Name = "gBoxAuditoria";
            gBoxAuditoria.Size = new Size(729, 499);
            gBoxAuditoria.TabIndex = 0;
            gBoxAuditoria.TabStop = false;
            gBoxAuditoria.Text = "Auditoría";
            // 
            // btnAuditoriaLoginLogout
            // 
            btnAuditoriaLoginLogout.Location = new Point(408, 82);
            btnAuditoriaLoginLogout.Name = "btnAuditoriaLoginLogout";
            btnAuditoriaLoginLogout.Size = new Size(212, 39);
            btnAuditoriaLoginLogout.TabIndex = 4;
            btnAuditoriaLoginLogout.Tag = "30";
            btnAuditoriaLoginLogout.Text = "Auditoría de Login/Logout";
            btnAuditoriaLoginLogout.UseVisualStyleBackColor = true;
            btnAuditoriaLoginLogout.Click += btnAuditoriaLoginLogout_Click;
            // 
            // btnAuditoriaAlumnos
            // 
            btnAuditoriaAlumnos.Location = new Point(93, 82);
            btnAuditoriaAlumnos.Name = "btnAuditoriaAlumnos";
            btnAuditoriaAlumnos.Size = new Size(212, 39);
            btnAuditoriaAlumnos.TabIndex = 3;
            btnAuditoriaAlumnos.Tag = "29";
            btnAuditoriaAlumnos.Text = "Auditoría de Alumnos";
            btnAuditoriaAlumnos.UseVisualStyleBackColor = true;
            btnAuditoriaAlumnos.Click += btnAuditoriaAlumnos_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 566);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FormAuditoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 601);
            Controls.Add(btnVolver);
            Controls.Add(gBoxAuditoria);
            Name = "FormAuditoria";
            Text = "FormAuditoria";
            FormClosed += FormAuditoria_FormClosed;
            Load += FormAuditoria_Load;
            gBoxAuditoria.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gBoxAuditoria;
        private Button btnVolver;
        private Button btnAuditoriaLoginLogout;
        private Button btnAuditoriaAlumnos;
    }
}