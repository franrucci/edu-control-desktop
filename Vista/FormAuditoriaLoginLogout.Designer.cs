namespace Vista
{
    partial class FormAuditoriaLoginLogout
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
            gBoxAuditoriaLoginLogout = new GroupBox();
            btnDescargarComprobante = new Button();
            dgvAuditoriaLoginLogout = new DataGridView();
            btnVolver = new Button();
            gBoxAuditoriaLoginLogout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoriaLoginLogout).BeginInit();
            SuspendLayout();
            // 
            // gBoxAuditoriaLoginLogout
            // 
            gBoxAuditoriaLoginLogout.Controls.Add(btnDescargarComprobante);
            gBoxAuditoriaLoginLogout.Controls.Add(dgvAuditoriaLoginLogout);
            gBoxAuditoriaLoginLogout.Location = new Point(12, 12);
            gBoxAuditoriaLoginLogout.Name = "gBoxAuditoriaLoginLogout";
            gBoxAuditoriaLoginLogout.Size = new Size(729, 481);
            gBoxAuditoriaLoginLogout.TabIndex = 1;
            gBoxAuditoriaLoginLogout.TabStop = false;
            gBoxAuditoriaLoginLogout.Text = "Auditoría Login/Logout";
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
            // dgvAuditoriaLoginLogout
            // 
            dgvAuditoriaLoginLogout.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoriaLoginLogout.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditoriaLoginLogout.Location = new Point(6, 22);
            dgvAuditoriaLoginLogout.Name = "dgvAuditoriaLoginLogout";
            dgvAuditoriaLoginLogout.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditoriaLoginLogout.Size = new Size(717, 356);
            dgvAuditoriaLoginLogout.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 566);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 2;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FormAuditoriaLoginLogout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 601);
            Controls.Add(btnVolver);
            Controls.Add(gBoxAuditoriaLoginLogout);
            Name = "FormAuditoriaLoginLogout";
            Text = "FormAuditoriaLoginLogout";
            Load += FormAuditoriaLoginLogout_Load;
            gBoxAuditoriaLoginLogout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAuditoriaLoginLogout).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gBoxAuditoriaLoginLogout;
        private Button btnDescargarComprobante;
        private DataGridView dgvAuditoriaLoginLogout;
        private Button btnVolver;
    }
}