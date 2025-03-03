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
    public partial class FormModificarUsuarioSeleccionado : Form
    {
        Usuario usuario;
        public FormModificarUsuarioSeleccionado(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombreUsuario.Text = usuario.Usuario1;
            txtContraseña.Text = "";
            txtContraseña.PasswordChar = '*';
            txtEmail.Text = usuario.Email;
            CargarCombo();

            // Busca y selecciona el rol correspondiente al usuario
            foreach (var item in cmbRol.Items)
            {
                if (((RolUsuario)item).RolUsuarioId == usuario.RolId)
                {
                    cmbRol.SelectedItem = item;
                    break;
                }
            }
        }

        private void CargarCombo()
        {
            try
            {
                var Lst = ControladoraRoles.Instancia.ComboRol();
                cmbRol.DataSource = Lst;
                cmbRol.DisplayMember = "RolNombre";
                cmbRol.ValueMember = "RolUsuarioId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                MessageBox.Show("Debe ingresar el nombre de usuario.");
                return false;
            }
            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el rol.");
                return false;
            }
            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool usuarioValido = ControladoraUsuarios.Instancia.ValidarUsuario(txtNombreUsuario.Text, usuario.UsuarioId);
            bool emailValido = ControladoraUsuarios.Instancia.ValidarEmail(txtEmail.Text, usuario.UsuarioId);

            if (ValidarDatos())
            {
                if (usuarioValido)
                {
                    if (emailValido)
                    {
                        usuario.Usuario1 = txtNombreUsuario.Text;

                        // Obtiene la nueva contraseña ingresada por el usuario
                        // Si no se ha ingresado nada, se pasa una cadena vacía
                        string nuevaContraseña = string.IsNullOrWhiteSpace(txtContraseña.Text) ? string.Empty : txtContraseña.Text;

                        usuario.RolId = (int)cmbRol.SelectedValue;

                        var mensaje = ControladoraUsuarios.Instancia.ModificarUsuario(usuario, nuevaContraseña);
                        MessageBox.Show(mensaje);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: El E-mail ya está registrado en el sistema");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Debe elegir otro nombre de usuario.");
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
