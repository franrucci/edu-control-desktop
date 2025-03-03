using Controladora;
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
    public partial class FormModificarAlumno : Form
    {
        Alumno alumno;
        int idUsu;
        public FormModificarAlumno(Alumno alumno1, int idUsu)
        {
            InitializeComponent();
            alumno = alumno1;
            this.idUsu = idUsu;
            InicializarCmb();
            CargarDatos();
        }

        private void InicializarCmb()
        {
            cmbSexo.DataSource = ControladoraSexos.Instancia.ListarSexos();
            cmbSexo.DisplayMember = "Nombre";

            cmbGradoAcademico.DataSource = ControladoraGradosAcademicos.Instancia.ListarGrados();
            cmbGradoAcademico.DisplayMember = "NumGrado";
        }

        private void ConfigurarCmb(Alumno alumno)
        {
            // Busca y selecciona el sexo correspondiente al alumno
            foreach (var item in cmbSexo.Items)
            {
                if (((Sexo)item).SexoId == alumno.SexoId)
                {
                    cmbSexo.SelectedItem = item;
                    break;
                }
            }

            // Busca y selecciona el grado correspondiente al alumno
            foreach (var item in cmbGradoAcademico.Items)
            {
                if (((GradoAcademico)item).GradoAcademicoId == alumno.GradoAcademicoId)
                {
                    cmbGradoAcademico.SelectedItem = item;
                    break;
                }
            }
        }

        private void CargarDatos()
        {
            txtNombre.Text = alumno.Nombre;
            txtApellido.Text = alumno.Apellido;
            txtDni.Text = alumno.Dni;
            dtpFechaDeNacimiento.Value = alumno.FechaDeNacimiento;
            txtDomicilio.Text = alumno.Domicilio;
            txtLocalidad.Text = alumno.Localidad;
            txtCodigoPostal.Text = alumno.CodigoPostal.ToString();
            txtProvincia.Text = alumno.Provincia;
            txtPais.Text = alumno.Pais;
            txtEmail.Text = alumno.Email;
            ConfigurarCmb(alumno);
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
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
                alumno.GradoAcademico = (GradoAcademico)cmbGradoAcademico.SelectedItem;

                var mensaje = ControladoraAlumnos.Instancia.ModificarAlumno(alumno, alumno.PersonaId, idUsu);
                MessageBox.Show(mensaje);
                this.Close();
            }
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
