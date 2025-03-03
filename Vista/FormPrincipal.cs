using Controladora;
using Entidades;
using Microsoft.VisualBasic.ApplicationServices;
using Modelo;
using Org.BouncyCastle.Crypto;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vista
{
    public partial class FormPrincipal : Form
    {
        int IdRol, IdUsu;
        public FormPrincipal(string pUser, int pIdRol, int pIdUsu)
        {
            InitializeComponent();
            IdRol = pIdRol;
            IdUsu = pIdUsu;
            CargarTxt();
        }

        private void CargarTxt()
        {
            var usuario = ControladoraUsuarios.Instancia.RetornarUsuario(IdUsu);
            lblUser.Text = "Usuario: " + usuario.Usuario1;

            var rol = ControladoraRoles.Instancia.RetornarRol(usuario.RolId);
            lblRol.Text = "Rol: " + rol.RolNombre;
        }


        #region Funcionalidades del formulario.
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCiclosAcademicos_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<FormCiclosAcademicos>(IdRol, IdUsu);
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormAlumnosTotal>(IdRol, IdUsu);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormAdministrarUsuarios>(IdRol, IdUsu);
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que quiere cerrar sesión?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnMiUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormMiUsuario>(IdRol, IdUsu);
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormAuditoria>(IdRol, IdUsu);
        }
        #endregion


        // METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        public void AbrirFormulario<MiForm>(int idRol, int idUsu) where MiForm : Form, new()
        {
            // Cierra los formularios abiertos, excepto "FormPrincipal" y "FormLogin"
            Form[] OpenForms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form thisForm in OpenForms)
            {
                if (thisForm.Name != "FormPrincipal" && thisForm.Name != "FormLogin")
                    thisForm.Close();
            }

            Form formulario;
            formulario = panelFormularios.Controls.OfType<MiForm>().FirstOrDefault(); // Busca el formulario en la colección

            if (formulario == null) // Si el formulario no está abierto
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;

                // Pasar el idRol al formulario
                if (formulario is IFormularioConRol rolForm)
                {
                    rolForm.SetIdRol(idRol, idUsu);
                }

                panelFormularios.Controls.Add(formulario); // Añade el formulario al panel
                panelFormularios.Tag = formulario; // Asocia el formulario al panel
                formulario.Show(); // Muestra el formulario
                formulario.BringToFront(); // Trae al frente el formulario
            }
            else // Si el formulario ya está abierto
            {
                formulario.BringToFront(); // Solo lo trae al frente
            }
        }
    }
}
