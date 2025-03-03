namespace Vista
{
    partial class FormPagarCuotaMensual
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
            gBoxCuotas = new GroupBox();
            btnPagarCuota = new Button();
            dgvCuotasImpagas = new DataGridView();
            btnVolver = new Button();
            gBoxCuotasPagas = new GroupBox();
            btnDescargarComprobante = new Button();
            dgvCuotasPagas = new DataGridView();
            gBoxCuotas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCuotasImpagas).BeginInit();
            gBoxCuotasPagas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCuotasPagas).BeginInit();
            SuspendLayout();
            // 
            // gBoxCuotas
            // 
            gBoxCuotas.Controls.Add(btnPagarCuota);
            gBoxCuotas.Controls.Add(dgvCuotasImpagas);
            gBoxCuotas.Location = new Point(12, 12);
            gBoxCuotas.Name = "gBoxCuotas";
            gBoxCuotas.Size = new Size(729, 264);
            gBoxCuotas.TabIndex = 0;
            gBoxCuotas.TabStop = false;
            gBoxCuotas.Text = "Cuotas Impagas";
            // 
            // btnPagarCuota
            // 
            btnPagarCuota.Location = new Point(491, 22);
            btnPagarCuota.Name = "btnPagarCuota";
            btnPagarCuota.Size = new Size(232, 31);
            btnPagarCuota.TabIndex = 2;
            btnPagarCuota.Text = "Pagar Cuota";
            btnPagarCuota.UseVisualStyleBackColor = true;
            btnPagarCuota.Click += btnPagarCuota_Click;
            // 
            // dgvCuotasImpagas
            // 
            dgvCuotasImpagas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCuotasImpagas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCuotasImpagas.Location = new Point(6, 22);
            dgvCuotasImpagas.Name = "dgvCuotasImpagas";
            dgvCuotasImpagas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuotasImpagas.Size = new Size(459, 224);
            dgvCuotasImpagas.TabIndex = 0;
            dgvCuotasImpagas.SelectionChanged += dgvCuotasImpagas_SelectionChanged;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 566);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // gBoxCuotasPagas
            // 
            gBoxCuotasPagas.Controls.Add(btnDescargarComprobante);
            gBoxCuotasPagas.Controls.Add(dgvCuotasPagas);
            gBoxCuotasPagas.Location = new Point(12, 289);
            gBoxCuotasPagas.Name = "gBoxCuotasPagas";
            gBoxCuotasPagas.Size = new Size(729, 264);
            gBoxCuotasPagas.TabIndex = 4;
            gBoxCuotasPagas.TabStop = false;
            gBoxCuotasPagas.Text = "Cuotas Pagas";
            // 
            // btnDescargarComprobante
            // 
            btnDescargarComprobante.Location = new Point(491, 22);
            btnDescargarComprobante.Name = "btnDescargarComprobante";
            btnDescargarComprobante.Size = new Size(232, 31);
            btnDescargarComprobante.TabIndex = 2;
            btnDescargarComprobante.Text = "Descargar comprobante de pago";
            btnDescargarComprobante.UseVisualStyleBackColor = true;
            btnDescargarComprobante.Click += btnDescargarComprobante_Click;
            // 
            // dgvCuotasPagas
            // 
            dgvCuotasPagas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCuotasPagas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCuotasPagas.Location = new Point(6, 22);
            dgvCuotasPagas.Name = "dgvCuotasPagas";
            dgvCuotasPagas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuotasPagas.Size = new Size(459, 224);
            dgvCuotasPagas.TabIndex = 0;
            // 
            // FormPagarCuotaMensual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 601);
            Controls.Add(gBoxCuotasPagas);
            Controls.Add(btnVolver);
            Controls.Add(gBoxCuotas);
            Name = "FormPagarCuotaMensual";
            Text = "FormPagarCuotaMensual";
            gBoxCuotas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCuotasImpagas).EndInit();
            gBoxCuotasPagas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCuotasPagas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gBoxCuotas;
        private DataGridView dgvCuotasImpagas;
        private Button btnVolver;
        private Button btnPagarCuota;
        private GroupBox gBoxCuotasPagas;
        private Button btnDescargarComprobante;
        private DataGridView dgvCuotasPagas;
    }
}