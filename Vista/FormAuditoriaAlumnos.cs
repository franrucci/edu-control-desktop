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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Vista
{
    public partial class FormAuditoriaAlumnos : Form
    {
        public FormAuditoriaAlumnos()
        {
            InitializeComponent();
        }

        private void FormAuditoriaAlumnos_Load(object sender, EventArgs e)
        {
            CargarAuditorias();
        }

        private void CargarAuditorias()
        {
            List<AlumnoAuditoria> auditorias = ControladoraAuditorias.Instancia.ObtenerAuditorias();

            // Para cada auditoría, obtener el alumno y el usuario por sus respectivos IDs
            var auditoriasConDetalles = auditorias.Select(a =>
            {
                Alumno alumno = a.PersonaId.HasValue? ControladoraAlumnos.Instancia.ObtenerAlumnoPorId(a.PersonaId.Value): null;

                Usuario usuario = ControladoraUsuarios.Instancia.RetornarUsuario(a.UsuarioId);

                return new
                {
                    ID = a.AlumnoAuditoriaId,
                    Alumno = alumno != null ? $"{alumno.Nombre} {alumno.Apellido}" : "Alumno eliminado",
                    Usuario = usuario?.Usuario1 ?? "Desconocido",
                    Fecha = a.FechaHora,
                    Accion = a.Accion,
                    Datos_Anteriores = a.DatosAnteriores ?? "No disponible",
                    Datos_Nuevos = a.DatosNuevos ?? "No disponible"
                };
            }).ToList();

            dgvAuditoriaAlumnos.DataSource = auditoriasConDetalles;

            dgvAuditoriaAlumnos.Columns["ID"].Visible = false;
            dgvAuditoriaAlumnos.Columns["Alumno"].HeaderText = "Alumno";
            dgvAuditoriaAlumnos.Columns["Usuario"].HeaderText = "Usuario";
            dgvAuditoriaAlumnos.Columns["Fecha"].HeaderText = "Fecha y Hora";
            dgvAuditoriaAlumnos.Columns["Accion"].HeaderText = "Acción";
            dgvAuditoriaAlumnos.Columns["Datos_Anteriores"].Visible = false;
            dgvAuditoriaAlumnos.Columns["Datos_Nuevos"].Visible = false;
        }


        private void btnDescargarComprobante_Click(object sender, EventArgs e)
        {
            if (dgvAuditoriaAlumnos.SelectedRows.Count > 0)
            {
                var auditoriaSeleccionada = (dynamic)dgvAuditoriaAlumnos.SelectedRows[0].DataBoundItem;

                SaveFileDialog guardar = new SaveFileDialog();
                guardar.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Templates\plantillaComprobanteAuditoriaAlumnos.html");
                string paginahtml_texto = File.ReadAllText(ruta);

                paginahtml_texto = paginahtml_texto.Replace("@ALUMNO", auditoriaSeleccionada.Alumno);
                paginahtml_texto = paginahtml_texto.Replace("@USUARIO", auditoriaSeleccionada.Usuario);
                paginahtml_texto = paginahtml_texto.Replace("@FECHA_HORA", auditoriaSeleccionada.Fecha.ToString("dd/MM/yyyy HH:mm:ss"));
                paginahtml_texto = paginahtml_texto.Replace("@ACCION", auditoriaSeleccionada.Accion);

                // Datos Anteriores
                if (auditoriaSeleccionada.Accion == "Registro de alumno")
                {
                    paginahtml_texto = paginahtml_texto.Replace("@CICLO_ACADEMICO_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@GRADO_ACADEMICO_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@NOMBRE_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@APELLIDO_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@DNI_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@FECHA_NACIMIENTO_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@DOMICILIO_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@LOCALIDAD_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@CODIGO_POSTAL_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@PROVINCIA_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@PAIS_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@EMAIL_ANTERIOR", "No existen datos anteriores")
                                                       .Replace("@SEXO_ANTERIOR", "No existen datos anteriores");
                }
                else
                {
                    var datosAnteriores = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(auditoriaSeleccionada.Datos_Anteriores);

                    if (datosAnteriores != null)
                    {
                        string cicloAcademicoAnterior = datosAnteriores.ContainsKey("CicloAcademico") ? datosAnteriores["CicloAcademico"]?["Año"]?.ToString() ?? "No disponible" : "No disponible";
                        string gradoAcademicoAnterior = datosAnteriores.ContainsKey("GradoAcademico") ? datosAnteriores["GradoAcademico"]?["NumGrado"]?.ToString() ?? "No disponible" : "No disponible";
                        string nombreAnterior = datosAnteriores.ContainsKey("Nombre") ? datosAnteriores["Nombre"]?.ToString() ?? "No disponible" : "No disponible";
                        string apellidoAnterior = datosAnteriores.ContainsKey("Apellido") ? datosAnteriores["Apellido"]?.ToString() ?? "No disponible" : "No disponible";
                        string dniAnterior = datosAnteriores.ContainsKey("Dni") ? datosAnteriores["Dni"]?.ToString() ?? "No disponible" : "No disponible";
                        string fechaNacimientoAnterior = datosAnteriores.ContainsKey("FechaDeNacimiento") ? DateTime.Parse(datosAnteriores["FechaDeNacimiento"]?.ToString() ?? "01/01/0001").ToString("dd/MM/yyyy") : "No disponible";
                        string domicilioAnterior = datosAnteriores.ContainsKey("Domicilio") ? datosAnteriores["Domicilio"]?.ToString() ?? "No disponible" : "No disponible";
                        string localidadAnterior = datosAnteriores.ContainsKey("Localidad") ? datosAnteriores["Localidad"]?.ToString() ?? "No disponible" : "No disponible";
                        string codigoPostalAnterior = datosAnteriores.ContainsKey("CodigoPostal") ? datosAnteriores["CodigoPostal"]?.ToString() ?? "No disponible" : "No disponible";
                        string provinciaAnterior = datosAnteriores.ContainsKey("Provincia") ? datosAnteriores["Provincia"]?.ToString() ?? "No disponible" : "No disponible";
                        string paisAnterior = datosAnteriores.ContainsKey("Pais") ? datosAnteriores["Pais"]?.ToString() ?? "No disponible" : "No disponible";
                        string emailAnterior = datosAnteriores.ContainsKey("Email") ? datosAnteriores["Email"]?.ToString() ?? "No disponible" : "No disponible";
                        string sexoAnterior = datosAnteriores.ContainsKey("Sexo")
                            ? (datosAnteriores["Sexo"] is JObject ? datosAnteriores["Sexo"]?["Nombre"]?.ToString() : datosAnteriores["Sexo"]?.ToString()) ?? "No disponible"
                            : "No disponible";


                        paginahtml_texto = paginahtml_texto.Replace("@CICLO_ACADEMICO_ANTERIOR", cicloAcademicoAnterior)
                                                           .Replace("@GRADO_ACADEMICO_ANTERIOR", gradoAcademicoAnterior)
                                                           .Replace("@NOMBRE_ANTERIOR", nombreAnterior)
                                                           .Replace("@APELLIDO_ANTERIOR", apellidoAnterior)
                                                           .Replace("@DNI_ANTERIOR", dniAnterior)
                                                           .Replace("@FECHA_NACIMIENTO_ANTERIOR", fechaNacimientoAnterior)
                                                           .Replace("@DOMICILIO_ANTERIOR", domicilioAnterior)
                                                           .Replace("@LOCALIDAD_ANTERIOR", localidadAnterior)
                                                           .Replace("@CODIGO_POSTAL_ANTERIOR", codigoPostalAnterior)
                                                           .Replace("@PROVINCIA_ANTERIOR", provinciaAnterior)
                                                           .Replace("@PAIS_ANTERIOR", paisAnterior)
                                                           .Replace("@EMAIL_ANTERIOR", emailAnterior)
                                                           .Replace("@SEXO_ANTERIOR", sexoAnterior);
                    }
                    else
                    {
                        MessageBox.Show("Error: No se encontraron datos anteriores.");
                    }
                }

                // Datos Nuevos
                if (!string.IsNullOrEmpty(auditoriaSeleccionada.Datos_Nuevos) && auditoriaSeleccionada.Datos_Nuevos != "No disponible")
                {
                    var datosNuevos = JsonConvert.DeserializeObject<Dictionary<string, object>>(auditoriaSeleccionada.Datos_Nuevos);

                    paginahtml_texto = paginahtml_texto.Replace("@CICLO_ACADEMICO", datosNuevos.ContainsKey("CicloAcademico") ? datosNuevos["CicloAcademico"]["Año"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@GRADO_ACADEMICO", datosNuevos.ContainsKey("GradoAcademico") ? datosNuevos["GradoAcademico"]["NumGrado"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", datosNuevos.ContainsKey("Nombre") ? datosNuevos["Nombre"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@APELLIDO", datosNuevos.ContainsKey("Apellido") ? datosNuevos["Apellido"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@DNI", datosNuevos.ContainsKey("Dni") ? datosNuevos["Dni"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_NACIMIENTO", datosNuevos.ContainsKey("FechaDeNacimiento") ? DateTime.Parse(datosNuevos["FechaDeNacimiento"].ToString()).ToString("dd/MM/yyyy") : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO", datosNuevos.ContainsKey("Domicilio") ? datosNuevos["Domicilio"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@LOCALIDAD", datosNuevos.ContainsKey("Localidad") ? datosNuevos["Localidad"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@CODIGO_POSTAL", datosNuevos.ContainsKey("CodigoPostal") ? datosNuevos["CodigoPostal"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@PROVINCIA", datosNuevos.ContainsKey("Provincia") ? datosNuevos["Provincia"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@PAIS", datosNuevos.ContainsKey("Pais") ? datosNuevos["Pais"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@EMAIL", datosNuevos.ContainsKey("Email") ? datosNuevos["Email"].ToString() : "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@SEXO", datosNuevos.ContainsKey("Sexo") ? datosNuevos["Sexo"]["Nombre"].ToString() : "No disponible");
                }
                else
                {
                    paginahtml_texto = paginahtml_texto.Replace("@CICLO_ACADEMICO", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@GRADO_ACADEMICO", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@APELLIDO", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@DNI", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_NACIMIENTO", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@LOCALIDAD", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@CODIGO_POSTAL", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@PROVINCIA", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@PAIS", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@EMAIL", "No disponible");
                    paginahtml_texto = paginahtml_texto.Replace("@SEXO", "No disponible");
                }

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
