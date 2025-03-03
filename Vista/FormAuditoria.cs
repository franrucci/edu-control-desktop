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
    public partial class FormAuditoria : Form, IFormularioConRol
    {
        int idRol;
        int idUsu;

        public FormAuditoria()
        {
            InitializeComponent();
        }

        public void SetIdRol(int idRol, int idUsu)
        {
            this.FormClosed += FormAuditoria_FormClosed;
            this.idRol = idRol;
        }
        private void FormAuditoria_Load(object sender, EventArgs e)
        {
            ConsultarRol(this, idRol);
        }

        private void FormAuditoria_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        public void ConsultarRol(Form pForm, int pIdRol)
        {
            var LstOp = ControladoraPermisos.Instancia.SelectOpcion(pIdRol);
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
                        // Comparar el OpcionId con el Tag del botón
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

                // Si el control es un contenedor recorre sus controles también
                if (c.HasChildren)
                {
                    RecorrerControles(c.Controls, LstOp);
                }
            }
        }

        private void btnAuditoriaAlumnos_Click(object sender, EventArgs e)
        {
            FormAuditoriaAlumnos formAuditoriaAlumnos = new FormAuditoriaAlumnos();

            AddOwnedForm(formAuditoriaAlumnos);

            formAuditoriaAlumnos.FormBorderStyle = FormBorderStyle.None;
            formAuditoriaAlumnos.TopLevel = false;
            formAuditoriaAlumnos.Dock = DockStyle.Fill;

            this.Controls.Add(formAuditoriaAlumnos);
            this.Tag = formAuditoriaAlumnos;

            formAuditoriaAlumnos.BringToFront();
            formAuditoriaAlumnos.Show();
        }

        private void btnAuditoriaLoginLogout_Click(object sender, EventArgs e)
        {
            FormAuditoriaLoginLogout formAuditoriaLoginLogout = new FormAuditoriaLoginLogout();

            AddOwnedForm(formAuditoriaLoginLogout);

            formAuditoriaLoginLogout.FormBorderStyle = FormBorderStyle.None;
            formAuditoriaLoginLogout.TopLevel = false;
            formAuditoriaLoginLogout.Dock = DockStyle.Fill;

            this.Controls.Add(formAuditoriaLoginLogout);
            this.Tag = formAuditoriaLoginLogout;

            formAuditoriaLoginLogout.BringToFront();
            formAuditoriaLoginLogout.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
