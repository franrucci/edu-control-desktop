using Controladora;
using Controladora.EmailService;
using Controladora.Patron_Facade;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FormRecuperarContraseña : Form
    {
        private readonly RecuperacionContraseñaFacade recuperacionContraseñaFacade;

        public FormRecuperarContraseña()
        {
            InitializeComponent();
            recuperacionContraseñaFacade = new RecuperacionContraseñaFacade();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "E-mail")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.LightGray;
            }
        }

        private bool EsEmailValido(string email)
        {
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patronEmail);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string mensajeError;

            if (!EsEmailValido(email))
            {
                MessageBox.Show("Error: El formato del correo electrónico ingresado no es válido.");
                return;
            }

            if (recuperacionContraseñaFacade.EnviarTokenRecuperacion(email, out mensajeError))
            {
                MessageBox.Show("Se ha enviado un correo electrónico con el token de recuperación.");
                this.Close();
            }
            else
            {
                MessageBox.Show(mensajeError);
            }
        }
    }
}
