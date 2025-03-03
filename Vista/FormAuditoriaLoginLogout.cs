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

namespace Vista
{
    public partial class FormAuditoriaLoginLogout : Form
    {
        public FormAuditoriaLoginLogout()
        {
            InitializeComponent();
        }

        private void FormAuditoriaLoginLogout_Load(object sender, EventArgs e)
        {
            CargarAuditorias();
        }

        private void CargarAuditorias()
        {
            List<LoginLogoutAuditoria> auditorias = ControladoraAuditorias.Instancia.ObtenerLoginLogoutAuditorias();

            // Para cada auditoría, obtengo el usuario por su ID
            var auditoriasConDetalles = auditorias.Select(a =>
            {
                Usuario usuario = ControladoraUsuarios.Instancia.RetornarUsuario(a.UsuarioId);
                RolUsuario rol = usuario != null ? ControladoraRoles.Instancia.RetornarRol(usuario.RolId) : null;


                return new
                {
                    ID = a.LoginLogoutAuditoriaId,
                    Usuario = usuario?.Usuario1 ?? "Desconocido",
                    Rol = rol?.RolNombre ?? "Sin Rol",
                    Fecha = a.FechaHora,
                    Accion = a.Accion
                };
            }).ToList();

            dgvAuditoriaLoginLogout.DataSource = auditoriasConDetalles;

            dgvAuditoriaLoginLogout.Columns["ID"].Visible = false;
            dgvAuditoriaLoginLogout.Columns["Usuario"].HeaderText = "Usuario";
            dgvAuditoriaLoginLogout.Columns["Fecha"].HeaderText = "Fecha y Hora";
            dgvAuditoriaLoginLogout.Columns["Accion"].HeaderText = "Acción";
        }

        private void btnDescargarComprobante_Click(object sender, EventArgs e)
        {
            if (dgvAuditoriaLoginLogout.SelectedRows.Count > 0)
            {
                var auditoriaSeleccionada = (dynamic)dgvAuditoriaLoginLogout.SelectedRows[0].DataBoundItem;

                SaveFileDialog guardar = new SaveFileDialog();
                guardar.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Templates\plantillaComprobanteAuditoriaLoginLogout.html");
                string paginahtml_texto = File.ReadAllText(ruta);

                paginahtml_texto = paginahtml_texto.Replace("@USUARIO", auditoriaSeleccionada.Usuario);
                paginahtml_texto = paginahtml_texto.Replace("@ROL", auditoriaSeleccionada.Rol);
                paginahtml_texto = paginahtml_texto.Replace("@FECHA_HORA", auditoriaSeleccionada.Fecha.ToString("dd/MM/yyyy HH:mm:ss"));
                paginahtml_texto = paginahtml_texto.Replace("@ACCION", auditoriaSeleccionada.Accion);

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
                MessageBox.Show("Error: Debe seleccionar una auditoría.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
