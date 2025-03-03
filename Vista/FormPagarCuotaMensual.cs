using Controladora;
using Entidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Vista
{
    public partial class FormPagarCuotaMensual : Form
    {
        Alumno alumno;
        Cuota cuotaSeleccionada;
        public FormPagarCuotaMensual(Alumno alumno1)
        {
            InitializeComponent();
            alumno = alumno1;
            ActualizarDgvCuotasImpagas();
            ActualizarDgvCuotasPagas();
        }

        public void ActualizarDgvCuotasImpagas()
        {
            var cuotasImpagas = ControladoraCuotasMensuales.Instancia.ObtenerCuotasImpagas(alumno);

            var cuotasConAño = cuotasImpagas.Select(c => new
            {
                c.CuotaId,
                c.Mes,
                Año = c.CicloAcademico.Año,
                Precio = c.Precio,
                Alumno = alumno.Nombre + " " + alumno.Apellido
            }).ToList();

            dgvCuotasImpagas.DataSource = null;
            dgvCuotasImpagas.DataSource = cuotasConAño;

            dgvCuotasImpagas.Columns["CuotaId"].Visible = false;
            dgvCuotasImpagas.Columns["Mes"].HeaderText = "Mes N°";
            dgvCuotasImpagas.Columns["Año"].HeaderText = "Año";
            dgvCuotasImpagas.Columns["Precio"].HeaderText = "Precio";
            dgvCuotasImpagas.Columns["Alumno"].HeaderText = "Alumno";
        }

        public void ActualizarDgvCuotasPagas()
        {
            var cuotasPagas = ControladoraCuotasMensuales.Instancia.ObtenerCuotasPagas(alumno);

            var cuotasConAño = cuotasPagas.Select(c => new
            {
                c.CuotaId,
                c.Mes,
                Año = c.CicloAcademico.Año,
                Precio = c.Precio,
                Alumno = alumno.Nombre + " " + alumno.Apellido
            }).ToList();

            dgvCuotasPagas.DataSource = null;
            dgvCuotasPagas.DataSource = cuotasConAño;

            dgvCuotasPagas.Columns["CuotaId"].Visible = false;
            dgvCuotasPagas.Columns["Precio"].Visible = false;
            dgvCuotasPagas.Columns["Mes"].HeaderText = "Mes N°";
            dgvCuotasPagas.Columns["Año"].HeaderText = "Año";
            dgvCuotasPagas.Columns["Alumno"].HeaderText = "Alumno";
        }

        private void dgvCuotasImpagas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCuotasImpagas.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCuotasImpagas.SelectedRows[0];

                int cuotaId = Convert.ToInt32(row.Cells["CuotaId"].Value);

                cuotaSeleccionada = ControladoraCuotasMensuales.Instancia.ObtenerCuotaPorId(cuotaId);

                if (cuotaSeleccionada == null)
                {
                    MessageBox.Show("Error: No se pudo cargar la información de la cuota.");
                }
            }
        }

        private void btnPagarCuota_Click(object sender, EventArgs e)
        {
            if (cuotaSeleccionada != null)
            {
                FormPagarCuota formPagarCuota = new FormPagarCuota(cuotaSeleccionada, alumno);
                formPagarCuota.Owner = this;
                formPagarCuota.FormBorderStyle = FormBorderStyle.None;
                formPagarCuota.TopLevel = false;
                formPagarCuota.Dock = DockStyle.Fill;
                this.Controls.Add(formPagarCuota);
                this.Tag = formPagarCuota;
                formPagarCuota.BringToFront();
                formPagarCuota.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnDescargarComprobante_Click(object sender, EventArgs e)
        {
            if (dgvCuotasPagas.SelectedRows.Count > 0)
            {
                int cuotaId = Convert.ToInt32(dgvCuotasPagas.SelectedRows[0].Cells["CuotaId"].Value);

                var pagoSeleccionado = ControladoraCuotasMensuales.Instancia.ObtenerPagoDeCuotaPorCuotaId(cuotaId, alumno.PersonaId);

                if (pagoSeleccionado == null)
                {
                    MessageBox.Show("Error: No se pudo cargar la información del pago.");
                    return;
                }

                SaveFileDialog guardar = new SaveFileDialog();
                guardar.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Templates\plantillaComprobantePagoCuota.html");
                string paginahtml_texto = File.ReadAllText(ruta);

                // Información del alumno
                paginahtml_texto = paginahtml_texto.Replace("@ALUMNO", pagoSeleccionado.Alumno.Nombre + " " + pagoSeleccionado.Alumno.Apellido);
                var GradoAcademico = ControladoraGradosAcademicos.Instancia.BuscarGrado(pagoSeleccionado.Alumno.GradoAcademicoId);
                paginahtml_texto = paginahtml_texto.Replace("@GRADO", GradoAcademico.NumGrado.ToString());
                paginahtml_texto = paginahtml_texto.Replace("@CICLO_ACADEMICO", pagoSeleccionado.Cuota.CicloAcademico.Año.ToString());

                // Información del pago
                paginahtml_texto = paginahtml_texto.Replace("@FECHA_PAGO", pagoSeleccionado.Fecha.ToString("dd/MM/yyyy"));
                paginahtml_texto = paginahtml_texto.Replace("@MONTO",pagoSeleccionado.MontoFinal.ToString("C", new CultureInfo("es-AR")));
                paginahtml_texto = paginahtml_texto.Replace("@ESTADO_CUOTA", pagoSeleccionado.EstadoCuota.Nombre);
                paginahtml_texto = paginahtml_texto.Replace("@TIPO_PAGO", pagoSeleccionado.Pago.TipoDePago.Nombre);
                paginahtml_texto = paginahtml_texto.Replace("@CODIGO_COMPROBANTE", pagoSeleccionado.Pago.CodigoDeComprobante);

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(new Phrase(""));

                        using (StringReader sr = new StringReader(paginahtml_texto))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }

                        pdfDoc.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un pago de cuota.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
