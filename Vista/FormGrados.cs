﻿using Controladora;
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
    public partial class FormGrados : Form
    {
        GradoAcademico gradoSeleccionado;
        CicloAcademico cicloAcademico;
        int idRol;
        public FormGrados(CicloAcademico ciclo1, int idRol)
        {
            InitializeComponent();
            ActualizarGrilla();
            cicloAcademico = ciclo1;
            this.idRol = idRol;
        }

        public void ActualizarGrilla()
        {
            dgvGrados.DataSource = null;
            dgvGrados.DataSource = ControladoraGradosAcademicos.Instancia.ListarGrados();

            dgvGrados.Columns["NumGrado"].HeaderText = "Grado";

            dgvGrados.Columns["CantDeMaterias"].Visible = false;
            dgvGrados.Columns["TipoDeCiclo"].Visible = false;
            dgvGrados.Columns["GradoAcademicoId"].Visible = false;
            dgvGrados.Columns["TipoDeCicloId"].Visible = false;
        }

        private void dgvGrados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGrados.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvGrados.SelectedRows[0];
                gradoSeleccionado = (GradoAcademico)row.DataBoundItem;
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (gradoSeleccionado != null)
            {
                FormAlumnos formAlumnos = new FormAlumnos(gradoSeleccionado, cicloAcademico, idRol);

                AddOwnedForm(formAlumnos);

                formAlumnos.FormBorderStyle = FormBorderStyle.None;
                formAlumnos.TopLevel = false;
                formAlumnos.Dock = DockStyle.Fill;

                this.Controls.Add(formAlumnos);
                this.Tag = formAlumnos;

                formAlumnos.BringToFront();
                formAlumnos.Show();
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un grado académico.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
