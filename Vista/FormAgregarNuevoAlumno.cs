﻿using Controladora;
using Entidades;
using Modelo.Patron_Strategy;
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
    public partial class FormAgregarNuevoAlumno : Form
    {
        CicloAcademico cicloAcademico;
        int idUsu;
        public FormAgregarNuevoAlumno(CicloAcademico cicloAcademico1, int idUsu)
        {
            cicloAcademico = cicloAcademico1;
            this.idUsu = idUsu;
            InitializeComponent();
            IncicializarCmb();
        }

        private void IncicializarCmb()
        {
            cmbSexo.DataSource = ControladoraSexos.Instancia.ListarSexos();
            cmbSexo.DisplayMember = "Nombre";

            cmbGradoAcademico.DataSource = ControladoraGradosAcademicos.Instancia.ListarGrados();
            cmbGradoAcademico.DisplayMember = "NumGrado";
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre.");
                return false;
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Debe ingresar el apellido");
                return false;
            }
            if (string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Debe ingresar el DNI.");
                return false;
            }
            if (string.IsNullOrEmpty(txtDomicilio.Text))
            {
                MessageBox.Show("Debe ingresar el domicilio.");
                return false;
            }
            if (string.IsNullOrEmpty(txtLocalidad.Text))
            {
                MessageBox.Show("Debe ingresar la localidad.");
                return false;
            }
            if (string.IsNullOrEmpty(txtCodigoPostal.Text))
            {
                MessageBox.Show("Debe ingresar el codigo postal.");
                return false;
            }
            if (string.IsNullOrEmpty(txtProvincia.Text))
            {
                MessageBox.Show("Debe ingresar la provincia.");
                return false;
            }
            if (string.IsNullOrEmpty(txtPais.Text))
            {
                MessageBox.Show("Debe ingresar el pais.");
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Debe ingresar el Email.");
                return false;
            }
            if (cmbSexo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el sexo.");
                return false;
            }
            return true;
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Alumno alumno = new Alumno();
                alumno.Nombre = txtNombre.Text;
                alumno.Apellido = txtApellido.Text;
                alumno.Dni = txtDni.Text;
                alumno.FechaDeNacimiento = dtpFechaDeNacimiento.Value;
                alumno.Domicilio = txtDomicilio.Text;
                alumno.Localidad = txtLocalidad.Text;
                alumno.CodigoPostal = Convert.ToInt32(txtCodigoPostal.Text);
                alumno.Provincia = txtProvincia.Text;
                alumno.Pais = txtPais.Text;
                alumno.Email = txtEmail.Text;
                alumno.Sexo = (Sexo)cmbSexo.SelectedItem;

                alumno.CicloAcademico = cicloAcademico;
                if (cmbGradoAcademico.SelectedItem != null)
                {
                    alumno.GradoAcademico = (GradoAcademico)cmbGradoAcademico.SelectedItem;

                    // Cambiar estrategia a AgregarAlumnoNuevoStrategy
                    ControladoraAlumnos.Instancia.CambiarEstrategia(new AgregarAlumnoNuevoStrategy());

                    string respuesta = ControladoraAlumnos.Instancia.Agregar(alumno, cicloAcademico, alumno.GradoAcademico, idUsu);
                    MessageBox.Show(respuesta);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: No hay ningun grado academico seleccionado.");
                }
            }
        }

        private void btnRegistrarAlumnoExistente_Click(object sender, EventArgs e)
        {
            if (cicloAcademico != null)
            {
                // Crea una instancia del formulario para registrar alumno existente
                FormRegistrarAlumnoExistente formRegistrarAlumnoExistente = new FormRegistrarAlumnoExistente(cicloAcademico, idUsu);

                // Asegura que el formulario sea hijo del formulario principal
                AddOwnedForm(formRegistrarAlumnoExistente);

                // Configura el formulario para que no tenga borde y se ajuste al contenedor principal
                formRegistrarAlumnoExistente.FormBorderStyle = FormBorderStyle.None;
                formRegistrarAlumnoExistente.TopLevel = false;
                formRegistrarAlumnoExistente.Dock = DockStyle.Fill;

                // Agrega el formulario al contenedor principal
                this.Controls.Add(formRegistrarAlumnoExistente);
                this.Tag = formRegistrarAlumnoExistente;

                // Muestra el formulario, lo pone al frente
                formRegistrarAlumnoExistente.BringToFront();
                formRegistrarAlumnoExistente.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDomicilio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtLocalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '_' && e.KeyChar != '@')
            {
                e.Handled = true;
            }
        }
    }
}
