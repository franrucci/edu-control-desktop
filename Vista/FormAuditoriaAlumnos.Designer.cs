namespace Vista
{
    partial class FormAuditoriaAlumnos
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
            gBoxAuditoriaAlumnos = new GroupBox();
            btnDescargarComprobante = new Button();
            dgvAuditoriaAlumnos = new DataGridView();
            btnVolver = new Button();
            gBoxAuditoriaAlumnos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoriaAlumnos).BeginInit();
            SuspendLayout();
            // 
            // gBoxAuditoriaAlumnos
            // 
            gBoxAuditoriaAlumnos.Controls.Add(btnDescargarComprobante);
            gBoxAuditoriaAlumnos.Controls.Add(dgvAuditoriaAlumnos);
            gBoxAuditoriaAlumnos.Location = new Point(12, 12);
            gBoxAuditoriaAlumnos.Name = "gBoxAuditoriaAlumnos";
            gBoxAuditoriaAlumnos.Size = new Size(729, 481);
            gBoxAuditoriaAlumnos.TabIndex = 0;
            gBoxAuditoriaAlumnos.TabStop = false;
            gBoxAuditoriaAlumnos.Text = "Auditoría de Alumnos";
            // 
            // btnDescargarComprobante
            // 
            btnDescargarComprobante.Location = new Point(261, 407);
            btnDescargarComprobante.Name = "btnDescargarComprobante";
            btnDescargarComprobante.Size = new Size(192, 41);
            btnDescargarComprobante.TabIndex = 1;
            btnDescargarComprobante.Text = "Descargar comprobante de auditoría - PDF";
            btnDescargarComprobante.UseVisualStyleBackColor = true;
            btnDescargarComprobante.Click += btnDescargarComprobante_Click;
            // 
            // dgvAuditoriaAlumnos
            // 
            dgvAuditoriaAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoriaAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditoriaAlumnos.Location = new Point(6, 22);
            dgvAuditoriaAlumnos.Name = "dgvAuditoriaAlumnos";
            dgvAuditoriaAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditoriaAlumnos.Size = new Size(717, 356);
            dgvAuditoriaAlumnos.TabIndex = 0;
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
            // FormAuditoriaAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 601);
            Controls.Add(btnVolver);
            Controls.Add(gBoxAuditoriaAlumnos);
            Name = "FormAuditoriaAlumnos";
            Text = "FormAuditoriaAlumnos";
            Load += FormAuditoriaAlumnos_Load;
            gBoxAuditoriaAlumnos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAuditoriaAlumnos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gBoxAuditoriaAlumnos;
        private DataGridView dgvAuditoriaAlumnos;
        private Button btnVolver;
        private Button btnDescargarComprobante;
    }
}