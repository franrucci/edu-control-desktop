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
    public partial class FormModificarUsuario : Form
    {
        Usuario usuarioSeleccionado;
        public FormModificarUsuario()
        {
            InitializeComponent();
        }

        private void FormModificarUsuario_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        public void ActualizarGrilla()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = ControladoraUsuarios.Instancia.ListarUsuarios();

            dgvUsuarios.Columns["Usuario1"].HeaderText = "Nombre de usuario";

            dgvUsuarios.Columns["UsuarioId"].Visible = false;
            dgvUsuarios.Columns["Password"].Visible = false;
            dgvUsuarios.Columns["RolId"].Visible = false;
            dgvUsuarios.Columns["RolUsuario"].Visible = false;
            dgvUsuarios.Columns["Salt"].Visible = false;
            dgvUsuarios.Columns["Token"].Visible = false;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                var usuarioDesdeGrilla = (Usuario)row.DataBoundItem;

                usuarioSeleccionado = ControladoraUsuarios.Instancia.RetornarUsuario(usuarioDesdeGrilla.UsuarioId);//ControladoraAlumnos.Instancia.ObtenerAlumnoPorId(alumnoDesdeGrilla.PersonaId);

                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Error: No se pudo cargar la información del alumno.");
                }
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado != null)
            {
                FormModificarUsuarioSeleccionado formModificarUsuarioSeleccionado = new FormModificarUsuarioSeleccionado(usuarioSeleccionado);

                AddOwnedForm(formModificarUsuarioSeleccionado);

                formModificarUsuarioSeleccionado.FormBorderStyle = FormBorderStyle.None;
                formModificarUsuarioSeleccionado.TopLevel = false;
                formModificarUsuarioSeleccionado.Dock = DockStyle.Fill;

                this.Controls.Add(formModificarUsuarioSeleccionado);
                this.Tag = formModificarUsuarioSeleccionado;

                formModificarUsuarioSeleccionado.BringToFront();
                formModificarUsuarioSeleccionado.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un usuario.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
