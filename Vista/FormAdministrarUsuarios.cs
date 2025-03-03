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
    public partial class FormAdministrarUsuarios : Form, IFormularioConRol
    {
        int idRol;
        public FormAdministrarUsuarios()
        {
            InitializeComponent();
        }

        public void SetIdRol(int idRol, int idUsu)
        {
            InitializeComponent();
            this.idRol = idRol;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistroDeRolUsuario_Click(object sender, EventArgs e)
        {
            FormRolesUsuarios formRolesUsuarios = new FormRolesUsuarios();

            // Asegura que el formulario sea hijo del formulario actual
            AddOwnedForm(formRolesUsuarios);

            // Configura el formulario para que se ajuste al panel y no tenga borde
            formRolesUsuarios.FormBorderStyle = FormBorderStyle.None;
            formRolesUsuarios.TopLevel = false;
            formRolesUsuarios.Dock = DockStyle.Fill;

            // Agrega el formulario al panel o contenedor principal
            this.Controls.Add(formRolesUsuarios);
            this.Tag = formRolesUsuarios;

            // Muestra el formulario, lo pone al frente y asegura que se vea correctamente
            formRolesUsuarios.BringToFront();
            formRolesUsuarios.Show();
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            FormRegistrarUsuario formRegistrarUsuario = new FormRegistrarUsuario();

            // Asegura que el formulario sea hijo del formulario actual
            AddOwnedForm(formRegistrarUsuario);

            // Configura el formulario para que se ajuste al panel y no tenga borde
            formRegistrarUsuario.FormBorderStyle = FormBorderStyle.None;
            formRegistrarUsuario.TopLevel = false;
            formRegistrarUsuario.Dock = DockStyle.Fill;

            // Agrega el formulario al panel
            this.Controls.Add(formRegistrarUsuario);
            this.Tag = formRegistrarUsuario;

            // Muestra el formulario
            formRegistrarUsuario.BringToFront();
            formRegistrarUsuario.Show();
        }

        private void FormAdministrarUsuarios_Load(object sender, EventArgs e)
        {
            ConsultarRol(this, idRol);
        }



        public void ConsultarRol(Form pForm, int pIdRol)
        {
            // Obtener la lista de permisos para el rol actual
            var LstOp = ControladoraPermisos.Instancia.SelectOpcion(pIdRol);

            // Recorre todos los controles del formulario
            RecorrerControles(pForm.Controls, LstOp);
        }
        private void RecorrerControles(Control.ControlCollection controles, List<Permiso> LstOp)
        {
            foreach (Control c in controles)
            {
                // Si el control es un botón
                if (c is Button)
                {
                    // Busca el permiso asociado al botón usando el Tag
                    foreach (Permiso opc in LstOp)
                    {
                        // Compara OpcionId con el Tag del botón
                        if (opc.OpcionId == Convert.ToInt32(c.Tag))
                        {
                            // Si el permiso no está permitido, deshabilita el botón
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
                if (c.HasChildren)
                {
                    RecorrerControles(c.Controls, LstOp);  // Llamada recursiva para recorrer los controles hijos
                }
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            FormModificarUsuario formModificarUsuario = new FormModificarUsuario();

            AddOwnedForm(formModificarUsuario);

            formModificarUsuario.FormBorderStyle = FormBorderStyle.None;
            formModificarUsuario.TopLevel = false;
            formModificarUsuario.Dock = DockStyle.Fill;

            this.Controls.Add(formModificarUsuario);
            this.Tag = formModificarUsuario;


            formModificarUsuario.BringToFront();
            formModificarUsuario.Show();
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            FormEliminarUsuario formEliminarUsuario = new FormEliminarUsuario();

            // Asegura que el formulario sea hijo del formulario actual
            AddOwnedForm(formEliminarUsuario);

            // Configura el formulario para que se ajuste al panel y no tenga borde
            formEliminarUsuario.FormBorderStyle = FormBorderStyle.None;
            formEliminarUsuario.TopLevel = false;
            formEliminarUsuario.Dock = DockStyle.Fill;

            // Agrega el formulario al panel o contenedor principal
            this.Controls.Add(formEliminarUsuario);
            this.Tag = formEliminarUsuario;

            // Muestra el formulario, lo pone al frente
            formEliminarUsuario.BringToFront();
            formEliminarUsuario.Show();
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            FormEliminarRol formEliminarRol = new FormEliminarRol();

            // Asegura que el formulario sea hijo del formulario actual
            AddOwnedForm(formEliminarRol);

            // Configura el formulario para que se ajuste al panel y no tenga borde
            formEliminarRol.FormBorderStyle = FormBorderStyle.None;
            formEliminarRol.TopLevel = false;
            formEliminarRol.Dock = DockStyle.Fill;

            // Agrega el formulario al panel o contenedor principal
            this.Controls.Add(formEliminarRol);
            this.Tag = formEliminarRol;

            // Muestra el formulario, lo pone al frente
            formEliminarRol.BringToFront();
            formEliminarRol.Show();
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            FormModificarRol formModificarRol = new FormModificarRol();

            // Asegura que el formulario sea hijo del formulario actual
            AddOwnedForm(formModificarRol);

            // Configura el formulario para que se ajuste al panel y no tenga borde
            formModificarRol.FormBorderStyle = FormBorderStyle.None;
            formModificarRol.TopLevel = false;
            formModificarRol.Dock = DockStyle.Fill;

            // Agrega el formulario al panel o contenedor principal
            this.Controls.Add(formModificarRol);
            this.Tag = formModificarRol;

            // Muestra el formulario, lo pone al frente
            formModificarRol.BringToFront();
            formModificarRol.Show();
        }
    }
}
