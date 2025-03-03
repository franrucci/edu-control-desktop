﻿using Controladora;
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
    public partial class FormAlumnosTotal : Form, IFormularioConRol
    {
        Alumno alumnoSeleccionado;
        int idRol;
        int idUsu;
        private List<Alumno> listaAlumnos;
        public FormAlumnosTotal()
        {
            InitializeComponent();
            ActualizarGrilla();
        }

        public void SetIdRol(int idRol, int idUsu)
        {
            ActualizarGrilla();
            this.idRol = idRol;
            this.idUsu = idUsu;
        }

        public void ActualizarGrilla()
        {
            listaAlumnos = ControladoraAlumnos.Instancia.ObtenerTodosLosAlumnos();
            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = listaAlumnos;

            dgvAlumnos.Columns["Nombre"].HeaderText = "Nombre";
            dgvAlumnos.Columns["Apellido"].HeaderText = "Apellido";
            dgvAlumnos.Columns["Dni"].HeaderText = "DNI";

            OcultarColumnas();
        }
        private void OcultarColumnas()
        {
            dgvAlumnos.Columns["PersonaId"].Visible = false;
            dgvAlumnos.Columns["FechaDeNacimiento"].Visible = false;
            dgvAlumnos.Columns["Domicilio"].Visible = false;
            dgvAlumnos.Columns["Localidad"].Visible = false;
            dgvAlumnos.Columns["CodigoPostal"].Visible = false;
            dgvAlumnos.Columns["Provincia"].Visible = false;
            dgvAlumnos.Columns["Pais"].Visible = false;
            dgvAlumnos.Columns["Email"].Visible = false;
            dgvAlumnos.Columns["Sexo"].Visible = false;
            dgvAlumnos.Columns["SexoId"].Visible = false;
            dgvAlumnos.Columns["CicloAcademico"].Visible = false;
            dgvAlumnos.Columns["CicloAcademicoId"].Visible = false;
            dgvAlumnos.Columns["GradoAcademico"].Visible = false;
            dgvAlumnos.Columns["GradoAcademicoId"].Visible = false;
        }
        private void FormAlumnosTotal_Load(object sender, EventArgs e)
        {
            ConsultarRol(this, idRol);
        }

        private void btnDatosPersonales_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                FormDatosPersonales formDatosPersonales = new FormDatosPersonales(alumnoSeleccionado);

                // Asegura que el formulario de datos personales sea hijo del formulario principal
                AddOwnedForm(formDatosPersonales);

                // Configura el formulario para que no tenga borde y se ajuste al contenedor principal
                formDatosPersonales.FormBorderStyle = FormBorderStyle.None;
                formDatosPersonales.TopLevel = false;
                formDatosPersonales.Dock = DockStyle.Fill;

                // Agrega el formulario al contenedor principal
                this.Controls.Add(formDatosPersonales);
                this.Tag = formDatosPersonales;

                // Muestra el formulario, lo pone al frente
                formDatosPersonales.BringToFront();
                formDatosPersonales.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnInformacionAcademica_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                FormInformacionAcademica formInformacionAcademica = new FormInformacionAcademica(alumnoSeleccionado);

                // Asegura que el formulario sea hijo del formulario principal
                AddOwnedForm(formInformacionAcademica);

                // Configura el formulario para que se ajuste al panel y no tenga borde
                formInformacionAcademica.FormBorderStyle = FormBorderStyle.None;
                formInformacionAcademica.TopLevel = false;
                formInformacionAcademica.Dock = DockStyle.Fill;

                // Agrega el formulario al contenedor principal
                this.Controls.Add(formInformacionAcademica);
                this.Tag = formInformacionAcademica;

                // Muestra el formulario, lo pone al frente
                formInformacionAcademica.BringToFront();
                formInformacionAcademica.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                FormEnviarEmail formEnviarEmail = new FormEnviarEmail(alumnoSeleccionado);

                // Asegura que el formulario sea hijo del formulario principal
                AddOwnedForm(formEnviarEmail);

                // Configura el formulario para que se ajuste al panel y no tenga borde
                formEnviarEmail.FormBorderStyle = FormBorderStyle.None;
                formEnviarEmail.TopLevel = false;
                formEnviarEmail.Dock = DockStyle.Fill;

                // Agrega el formulario al panel o contenedor principal
                this.Controls.Add(formEnviarEmail);
                this.Tag = formEnviarEmail;

                // Muestra el formulario, lo pone al frente
                formEnviarEmail.BringToFront();
                formEnviarEmail.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnPagarCuotas_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                FormPagarCuotaMensual formPagarCuotaMensual = new FormPagarCuotaMensual(alumnoSeleccionado);

                // Asegura que el formulario sea hijo del formulario principal
                AddOwnedForm(formPagarCuotaMensual);

                // Configura el formulario para que se ajuste al panel y no tenga borde
                formPagarCuotaMensual.FormBorderStyle = FormBorderStyle.None;
                formPagarCuotaMensual.TopLevel = false;
                formPagarCuotaMensual.Dock = DockStyle.Fill;

                // Agrega el formulario al panel o contenedor principal
                this.Controls.Add(formPagarCuotaMensual);
                this.Tag = formPagarCuotaMensual;

                // Muestra el formulario, lo pone al frente
                formPagarCuotaMensual.BringToFront();
                formPagarCuotaMensual.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnEliminarAlumno_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                var result = MessageBox.Show(
                    "Atención: En caso de eliminar el alumno se borrarán todos los registros relacionados con el mismo.\n¿Desea continuar?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var mensaje = ControladoraAlumnos.Instancia.EliminarAlumno(alumnoSeleccionado.PersonaId, idUsu);
                    MessageBox.Show(mensaje);
                    ActualizarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        private void btnModificarAlumno_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                FormModificarAlumno formModificarAlumno = new FormModificarAlumno(alumnoSeleccionado, idUsu);

                // Asegura que el formulario sea hijo del formulario principal
                AddOwnedForm(formModificarAlumno);

                // Configura el formulario para que se ajuste al panel y no tenga borde
                formModificarAlumno.FormBorderStyle = FormBorderStyle.None;
                formModificarAlumno.TopLevel = false;
                formModificarAlumno.Dock = DockStyle.Fill;

                // Agrega el formulario al panel o contenedor principal
                this.Controls.Add(formModificarAlumno);
                this.Tag = formModificarAlumno;

                // Muestra el formulario, lo pone al frente
                formModificarAlumno.BringToFront();
                formModificarAlumno.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un alumno.");
            }
        }

        public void ConsultarRol(Form pForm, int pIdRol)
        {
            // Obtiene la lista de permisos para el rol actual
            var LstOp = ControladoraPermisos.Instancia.SelectOpcion(pIdRol);

            // Recorre todos los controles del formulario, incluyendo los controles dentro de contenedores
            RecorrerControles(pForm.Controls, LstOp);
        }

        private void RecorrerControles(Control.ControlCollection controles, List<Permiso> LstOp)
        {
            foreach (Control c in controles)
            {
                if (c is Button)
                {
                    // Buscar el permiso asociado al botón usando el Tag
                    foreach (Permiso opc in LstOp)
                    {
                        // Compara la OpcionId con el Tag del botón
                        if (opc.OpcionId == Convert.ToInt32(c.Tag))
                        {
                            // Si el permiso no está permitido, deshabilitar el botón
                            if (!opc.Permitido)
                            {
                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = true;
                            }
                        }
                    }
                }

                // Si el control es un contenedor recorre sus controles también
                if (c.HasChildren)
                {
                    RecorrerControles(c.Controls, LstOp);
                }
            }
        }

        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAlumnos.SelectedRows[0];
                var alumnoDesdeGrilla = (Alumno)row.DataBoundItem;

                alumnoSeleccionado = ControladoraAlumnos.Instancia.ObtenerAlumnoPorId(alumnoDesdeGrilla.PersonaId);

                if (alumnoSeleccionado == null)
                {
                    MessageBox.Show("Error: No se pudo cargar la información del alumno.");
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Alumno> listaFiltrada;
            string filtro = txtBuscar.Text;

            if (filtro.Length >= 1)
            {
                listaFiltrada = listaAlumnos.FindAll(x => x.Dni.StartsWith(filtro));
            }
            else
            {
                listaFiltrada = listaAlumnos;
            }

            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = listaFiltrada;

            dgvAlumnos.Columns["Nombre"].HeaderText = "Nombre";
            dgvAlumnos.Columns["Apellido"].HeaderText = "Apellido";
            dgvAlumnos.Columns["Dni"].HeaderText = "DNI";
            OcultarColumnas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
