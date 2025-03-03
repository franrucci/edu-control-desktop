using Controladora;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FormModificarCuotaMensual : Form
    {
        Cuota cuota;
        CicloAcademico ciclo;
        public FormModificarCuotaMensual(Cuota cuota)
        {
            InitializeComponent();
            this.cuota = cuota;
        }

        private void FormModificarCuotaMensual_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            ciclo = ControladoraCiclosAcademicos.Instancia.ObtenerCicloAcademico(cuota.CicloAcademicoId);
            txtCicloAcademico.Text = ciclo.Año.ToString();
            txtMes.Text = cuota.Mes.ToString();
            txtPrecio.Text = cuota.Precio.ToString();
        }
        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Ingrese el precio de la cuota.");
                return false;
            }
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Cuota cuotaSeleccionada = new Cuota();

                cuotaSeleccionada.CicloAcademico = ciclo;
                cuotaSeleccionada.Mes = cuota.Mes;
                cuotaSeleccionada.Precio = Convert.ToDecimal(txtPrecio.Text);

                var mensaje = ControladoraCuotasMensuales.Instancia.ModificarCuota(cuotaSeleccionada);
                MessageBox.Show(mensaje);
                this.Close();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtPrecio.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
