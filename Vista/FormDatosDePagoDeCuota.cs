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
    public partial class FormDatosDePagoDeCuota : Form
    {
        Cuota cuota;
        Alumno alumno;
        DateTime fecha;
        decimal monto;
        int validacion;
        public FormDatosDePagoDeCuota(Cuota cuota1, Alumno alumno1, DateTime fecha1, int validacion1)
        {
            InitializeComponent();
            cuota = cuota1;
            alumno = alumno1;
            fecha = fecha1;
            validacion = validacion1;
            CargarDatos();
        }

        public void CargarDatos()
        {
            txtAlumno.Text = $"{alumno.Nombre} {alumno.Apellido}";
            txtCuotaMes.Text = cuota.Mes.ToString();
            txtCicloAcademicoAño.Text = cuota.CicloAcademico.Año.ToString();
            cmbTipoDePago.DataSource = ControladoraTiposDePago.Instancia.ListarTiposDePago();
            cmbTipoDePago.DisplayMember = "Nombre";
            dtpFecha.Value = fecha;

            if (validacion == 1) // Si validacion es 1, calcula el monto
            {
                monto = ControladoraPagosDeCuotas.Instancia.CalcularNuevoMonto(cuota, fecha);
                txtMonto.Text = monto.ToString("F2");
                txtMonto.Enabled = false;
            }
            else if (validacion == 2)// Si validacion es 2 no calcula el monto
            {
                txtMonto.Text = string.Empty;
                txtMonto.Enabled = true;
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombrePagador.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del pagador.");
                return false;
            }
            if (string.IsNullOrEmpty(txtApellidoPagador.Text))
            {
                MessageBox.Show("Debe ingresar el apellido del pagador.");
                return false;
            }
            if (string.IsNullOrEmpty(txtDniPagador.Text))
            {
                MessageBox.Show("Debe ingresar el DNI del pagador.");
                return false;
            }
            if (cmbTipoDePago.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el tipo de pago.");
                return false;
            }
            if (string.IsNullOrEmpty(txtCodigoDeComprobante.Text))
            {
                MessageBox.Show("Debe ingresar el codigo de comprobante.");
                return false;
            }
            return true;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Pago pago = new Pago();
                PagoDeCuota pagoDeCuota = new PagoDeCuota();

                pago.NombrePagador = txtNombrePagador.Text;
                pago.ApellidoPagador = txtApellidoPagador.Text;
                pago.DniPagador = txtDniPagador.Text;
                var tipoDePago = (TipoDePago)cmbTipoDePago.SelectedItem;
                pago.TipoDePagoId = tipoDePago.TipoDePagoId;
                pago.CodigoDeComprobante = txtCodigoDeComprobante.Text;

                pagoDeCuota.Codigo = Guid.NewGuid().ToString().Substring(0, 8);
                pagoDeCuota.Cuota = cuota;
                pagoDeCuota.Fecha = dtpFecha.Value;
                pagoDeCuota.Alumno = alumno;
                pagoDeCuota.EstadoCuotaId = 1;
                pagoDeCuota.Pago = pago;
                if (validacion == 1)
                {
                    pagoDeCuota.MontoFinal = monto;
                }
                else
                {
                    pagoDeCuota.MontoFinal = Convert.ToInt32(txtMonto.Text);
                }

                var mensaje = ControladoraPagosDeCuotas.Instancia.PagarCuota(pagoDeCuota);
                MessageBox.Show(mensaje);

                FormPagarCuotaMensual formPagarCuotaMensual = this.Owner as FormPagarCuotaMensual;

                if (formPagarCuotaMensual != null)
                {
                    formPagarCuotaMensual.ActualizarDgvCuotasImpagas();
                    formPagarCuotaMensual.ActualizarDgvCuotasPagas();
                }

                // Cierro los formularios intermedios
                this.Close();
                this.Owner?.Close();
            }
        }

        private void txtNombrePagador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellidoPagador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDniPagador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCodigoDeComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
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
