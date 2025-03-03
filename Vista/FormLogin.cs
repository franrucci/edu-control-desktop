using Controladora;
using Entidades;
using Modelo;
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
    public partial class FormLogin : Form
    {
        int IdRol, IdUsu;
        string nomUsu;
        public FormLogin()
        {
            InitializeComponent();
        }

        private bool ValidarCampos()
        {
            // Valida que los campos no estén vacíos o en su valor predeterminado
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text == "USUARIO")
            {
                mensajeError("Ingrese usuario");
                txtUsuario.Text = "USUARIO";
                txtUsuario.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtContraseña1.Text) || txtContraseña1.Text == "CONTRASEÑA")
            {
                mensajeError("Ingrese la contraseña");
                txtContraseña1.Text = "CONTRASEÑA";
                txtUsuario.Focus();
                return false;
            }

            var usuario = ControladoraUsuarios.Instancia.BuscarUsuario(txtUsuario.Text, txtContraseña1.Text);

            if (usuario.Count <= 0)
            {
                mensajeError("Error: Usuario no registrado o datos incorrectos.");
                return false;
            }

            // Asigno los valores si la búsqueda fue exitosa
            foreach (var usu in usuario)
            {
                IdRol = usu.RolId;
                IdUsu = usu.UsuarioId;
                nomUsu = usu.Usuario1;
            }

            return true;
        }

        private void mensajeError(string mensaje)
        {
            lblError.Text = " " + mensaje;
            lblError.Visible = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                // Obtengo el usuario que realizó el login
                var usuario = ControladoraUsuarios.Instancia.RetornarUsuario(IdUsu);

                // Registro la auditoría de login
                ControladoraAuditorias.Instancia.RegistrarLoginLogoutAuditoria(usuario, "Login");

                FormPrincipal m = new FormPrincipal(nomUsu, IdRol, IdUsu);
                m.Show();
                m.FormClosed += CerrarSesion;
                this.Hide();
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void CerrarSesion(object sender, FormClosedEventArgs e)
        {
            // Obtengo el usuario que realizó el logout
            var usuario = ControladoraUsuarios.Instancia.RetornarUsuario(IdUsu);

            // Registro la auditoría de logout
            ControladoraAuditorias.Instancia.RegistrarLoginLogoutAuditoria(usuario, "Logout");

            txtContraseña1.Text = "CONTRASEÑA";
            txtContraseña1.UseSystemPasswordChar = false;
            txtUsuario.Text = "USUARIO";
            lblError.Visible = false;
            this.Show();
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txtContraseña1_Enter(object sender, EventArgs e)
        {
            if (txtContraseña1.Text == "CONTRASEÑA")
            {
                txtContraseña1.Text = "";
                txtContraseña1.ForeColor = Color.LightGray;
                txtContraseña1.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseña1_Leave(object sender, EventArgs e)
        {
            if (txtContraseña1.Text == "")
            {
                txtContraseña1.Text = "CONTRASEÑA";
                txtContraseña1.ForeColor = Color.DimGray;
                txtContraseña1.UseSystemPasswordChar = false;
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void linkContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            FormRecuperarContraseña formRecuperarContraseña = new FormRecuperarContraseña();

            formRecuperarContraseña.FormClosed += (s, args) => this.Show();
            formRecuperarContraseña.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
