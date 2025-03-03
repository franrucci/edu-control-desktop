using Controladora;
using Entidades;
using Org.BouncyCastle.Crypto;
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
    public partial class FormMiUsuario : Form, IFormularioConRol
    {
        Usuario usuario;
        RolUsuario idRol;

        public FormMiUsuario()
        {
            InitializeComponent();
        }

        public void SetIdRol(int idRol, int idUsu)
        {
            Usuario usuarioEncontrado = ControladoraUsuarios.Instancia.RetornarUsuario(idUsu);
            usuario = usuarioEncontrado;
        }

        private void CargarDatosUsuario()
        {
            if (usuario != null)
            {
                txtNombreUsuario.Text = usuario.Usuario1;
                var rolEncontrado = ControladoraRoles.Instancia.RetornarRol(usuario.RolId);
                txtRol.Text = rolEncontrado.RolNombre;
            }
            else
            {
                MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMiUsuario_Load_1(object sender, EventArgs e)
        {
            CargarDatosUsuario();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña formCambiarContraseña = new FormCambiarContraseña(usuario);

            AddOwnedForm(formCambiarContraseña);

            formCambiarContraseña.FormBorderStyle = FormBorderStyle.None;
            formCambiarContraseña.TopLevel = false;
            formCambiarContraseña.Dock = DockStyle.Fill;

            this.Controls.Add(formCambiarContraseña);
            this.Tag = formCambiarContraseña;

            formCambiarContraseña.BringToFront();
            formCambiarContraseña.Show();
        }
    }
}
