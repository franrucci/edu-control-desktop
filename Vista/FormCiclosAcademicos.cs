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
    public partial class FormCiclosAcademicos : Form, IFormularioConRol
    {
        CicloAcademico cicloSeleccionado;
        int idRol;
        int idUsu;
        public FormCiclosAcademicos()
        {
            InitializeComponent();
            ActualizarGrilla();
            ConfigurarColumnas();
            this.FormClosed += FormCiclosAcademicos_FormClosed;
        }

        public void SetIdRol(int idRol)
        {
            ActualizarGrilla();
            this.FormClosed += FormCiclosAcademicos_FormClosed;
            this.idRol = idRol;
        }
        public void SetIdRol(int idRol, int idUsu)
        {
            ActualizarGrilla();
            this.FormClosed += FormCiclosAcademicos_FormClosed;
            this.idRol = idRol;
            this.idUsu = idUsu;
        }
        private void btnAgregarNuevoCiclo_Click(object sender, EventArgs e)
        {
            FormAgregarNuevoCicloAcademico formAgregarNuevoCicloAcademico = new FormAgregarNuevoCicloAcademico();

            AddOwnedForm(formAgregarNuevoCicloAcademico);

            formAgregarNuevoCicloAcademico.FormBorderStyle = FormBorderStyle.None;
            formAgregarNuevoCicloAcademico.TopLevel = false;
            formAgregarNuevoCicloAcademico.Dock = DockStyle.Fill;

            this.Controls.Add(formAgregarNuevoCicloAcademico);
            this.Tag = formAgregarNuevoCicloAcademico;

            formAgregarNuevoCicloAcademico.BringToFront();
            formAgregarNuevoCicloAcademico.Show();
        }

        public void ActualizarGrilla()
        {
            dgvCiclosAcademicos.DataSource = null;
            dgvCiclosAcademicos.DataSource = ControladoraCiclosAcademicos.Instancia.ListarCiclosAcademicos();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            dgvCiclosAcademicos.Columns["CicloAcademicoId"].Visible = false;
        }

        private void dgvCiclosAcademicos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCiclosAcademicos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCiclosAcademicos.SelectedRows[0];
                cicloSeleccionado = (CicloAcademico)row.DataBoundItem;
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (cicloSeleccionado != null)
            {
                FormCicloAcademicoSeleccionado formCicloAcademicoSeleccionado = new FormCicloAcademicoSeleccionado(cicloSeleccionado, idRol, idUsu);
                AddOwnedForm(formCicloAcademicoSeleccionado);
                formCicloAcademicoSeleccionado.FormBorderStyle = FormBorderStyle.None;
                formCicloAcademicoSeleccionado.TopLevel = false;
                formCicloAcademicoSeleccionado.Dock = DockStyle.Fill;
                this.Controls.Add(formCicloAcademicoSeleccionado);
                this.Tag = formCicloAcademicoSeleccionado;
                formCicloAcademicoSeleccionado.BringToFront();
                formCicloAcademicoSeleccionado.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo academico.");
            }
        }

        private void FormCiclosAcademicos_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FormCiclosAcademicos_Load(object sender, EventArgs e)
        {
            ConsultarRol(this, idRol);
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
                    // Busca el permiso asociado al botón usando el Tag
                    foreach (Permiso opc in LstOp)
                    {
                        // Compara el OpcionId con el Tag del botón
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

                // Si el control es un contenedor, lo recorre tambien
                if (c.HasChildren)
                {
                    RecorrerControles(c.Controls, LstOp);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
