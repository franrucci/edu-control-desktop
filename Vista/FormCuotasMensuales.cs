using Controladora;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FormCuotasMensuales : Form
    {
        CicloAcademico ciclo;
        Cuota cuotaSeleccionada;
        public FormCuotasMensuales(CicloAcademico cicloAcademico)
        {
            InitializeComponent();
            ciclo = cicloAcademico;
            ActualizarGrilla();
        }

        public void ActualizarGrilla()
        {
            dgvCuotasMensuales.DataSource = null;
            dgvCuotasMensuales.DataSource = ControladoraCuotasMensuales.Instancia.ListarCuotasPorAño(ciclo);
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            dgvCuotasMensuales.Columns["CicloAcademico"].Visible = false;
            dgvCuotasMensuales.Columns["CuotaId"].Visible = false;
            dgvCuotasMensuales.Columns["CicloAcademicoId"].Visible = false;

            dgvCuotasMensuales.Columns["Mes"].HeaderText = "Mes N°";
        }

        private void dgvCiclosAcademicos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCuotasMensuales.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCuotasMensuales.SelectedRows[0];
                cuotaSeleccionada = (Cuota)row.DataBoundItem;
            }
        }

        private void btnRegistrarNuevaCuota_Click(object sender, EventArgs e)
        {
            if (ciclo != null)
            {
                FormRegistrarNuevaCuotaMensual formRegistrarNuevaCuotaMensual = new FormRegistrarNuevaCuotaMensual(ciclo);

                AddOwnedForm(formRegistrarNuevaCuotaMensual);

                formRegistrarNuevaCuotaMensual.FormBorderStyle = FormBorderStyle.None;
                formRegistrarNuevaCuotaMensual.TopLevel = false;
                formRegistrarNuevaCuotaMensual.Dock = DockStyle.Fill;

                this.Controls.Add(formRegistrarNuevaCuotaMensual);
                this.Tag = formRegistrarNuevaCuotaMensual;

                formRegistrarNuevaCuotaMensual.BringToFront();
                formRegistrarNuevaCuotaMensual.Show();

                ActualizarGrilla();
                ConfigurarColumnas();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }

        private void btnModificarCuota_Click(object sender, EventArgs e)
        {
            if (cuotaSeleccionada != null)
            {
                FormModificarCuotaMensual formModificarCuotaMensual = new FormModificarCuotaMensual(cuotaSeleccionada);

                AddOwnedForm(formModificarCuotaMensual);

                formModificarCuotaMensual.FormBorderStyle = FormBorderStyle.None;
                formModificarCuotaMensual.TopLevel = false;
                formModificarCuotaMensual.Dock = DockStyle.Fill;

                this.Controls.Add(formModificarCuotaMensual);
                this.Tag = formModificarCuotaMensual;

                formModificarCuotaMensual.BringToFront();
                formModificarCuotaMensual.Show();

                ActualizarGrilla();
                ConfigurarColumnas();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar una cuota.");
            }
        }

        private void btnEliminarCuota_Click(object sender, EventArgs e)
        {
            if (cuotaSeleccionada != null)
            {
                string mensaje = ControladoraCuotasMensuales.Instancia.EliminarCuota(cuotaSeleccionada, ciclo);
                MessageBox.Show(mensaje);
                ActualizarGrilla();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar una cuota.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
