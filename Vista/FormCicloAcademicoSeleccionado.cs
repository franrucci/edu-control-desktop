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
    public partial class FormCicloAcademicoSeleccionado : Form
    {
        CicloAcademico cicloAcademico;
        int idRol;
        int idUsu;
        public FormCicloAcademicoSeleccionado(CicloAcademico cicloAcademico1, int idRol, int idUsu)
        {
            cicloAcademico = cicloAcademico1;
            InitializeComponent();
            ConfigurarDatos();
            this.idRol = idRol;
            this.idUsu = idUsu;
            this.FormClosed += FormCicloAcademicoSeleccionado_FormClosed;
        }

        private void ConfigurarDatos()
        {
            txtCicloAcademicoSeleccionado.Text = $"{cicloAcademico.Año}";
        }
        private void FormCicloAcademicoSeleccionado_Load(object sender, EventArgs e)
        {
            ConsultarRol(this, idRol);
        }
        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            if (cicloAcademico != null)
            {
                FormAgregarNuevoAlumno formAgregarNuevoAlumno = new FormAgregarNuevoAlumno(cicloAcademico, idUsu);

                AddOwnedForm(formAgregarNuevoAlumno);

                formAgregarNuevoAlumno.FormBorderStyle = FormBorderStyle.None;
                formAgregarNuevoAlumno.TopLevel = false;
                formAgregarNuevoAlumno.Dock = DockStyle.Fill;

                this.Controls.Add(formAgregarNuevoAlumno);
                this.Tag = formAgregarNuevoAlumno;

                formAgregarNuevoAlumno.BringToFront();
                formAgregarNuevoAlumno.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }
        private void btnAgregarTrimestre_Click(object sender, EventArgs e)
        {
            if (cicloAcademico != null)
            {
                FormAgregarTrimestre formAgregarTrimestre = new FormAgregarTrimestre(cicloAcademico);

                AddOwnedForm(formAgregarTrimestre);

                formAgregarTrimestre.FormBorderStyle = FormBorderStyle.None;
                formAgregarTrimestre.TopLevel = false;
                formAgregarTrimestre.Dock = DockStyle.Fill;

                this.Controls.Add(formAgregarTrimestre);
                this.Tag = formAgregarTrimestre;

                formAgregarTrimestre.BringToFront();
                formAgregarTrimestre.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }
        private void btnGradosAcademicos_Click(object sender, EventArgs e)
        {
            if (cicloAcademico != null)
            {
                FormGrados formGrados = new FormGrados(cicloAcademico, idRol);

                AddOwnedForm(formGrados);

                formGrados.FormBorderStyle = FormBorderStyle.None;
                formGrados.TopLevel = false;
                formGrados.Dock = DockStyle.Fill;

                this.Controls.Add(formGrados);
                this.Tag = formGrados;

                formGrados.BringToFront();
                formGrados.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }
        private void FormCicloAcademicoSeleccionado_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnCuotasMensuales_Click(object sender, EventArgs e)
        {
            if (cicloAcademico != null)
            {
                FormCuotasMensuales formCuotasMensuales = new FormCuotasMensuales(cicloAcademico);

                AddOwnedForm(formCuotasMensuales);

                formCuotasMensuales.FormBorderStyle = FormBorderStyle.None;
                formCuotasMensuales.TopLevel = false;
                formCuotasMensuales.Dock = DockStyle.Fill;

                this.Controls.Add(formCuotasMensuales);
                this.Tag = formCuotasMensuales;

                formCuotasMensuales.BringToFront();
                formCuotasMensuales.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un ciclo académico.");
            }
        }

        // método que recorre todos los botones del formulario y los habilita o deshabilita según el permiso
        public void ConsultarRol(Form pForm, int pIdRol)
        {
            // Obtengo la lista de permisos para el rol actual
            var LstOp = ControladoraPermisos.Instancia.SelectOpcion(pIdRol);

            // Recorre todos los controles del formulario
            foreach (Control c in pForm.Controls)
            {
                if (c is Button)
                {
                    // Busca el permiso asociado al botón usando el Tag
                    foreach (Permiso opc in LstOp)
                    {
                        // Compara la OpcionId con el Tag del botón
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
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
