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
    public partial class FormPagarCuota : Form
    {
        Cuota cuota;
        Alumno alumno;
        public FormPagarCuota(Cuota cuota1, Alumno alumno1)
        {
            InitializeComponent();
            cuota = cuota1;
            alumno = alumno1;
            CargarDatos();
        }

        public void CargarDatos()
        {
            txtAlumno.Text = $"{alumno.Nombre} {alumno.Apellido}";
            txtCuotaMes.Text = cuota.Mes.ToString();
            txtCicloAcademicoAño.Text = cuota.CicloAcademico.Año.ToString();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            int cuotaMes = cuota.Mes;
            int cuotaAño = cuota.CicloAcademico.Año;
            DateTime fechaSeleccionada = dtpFecha.Value;

            if ((fechaSeleccionada.Day > 20 && fechaSeleccionada.Month >= cuotaMes && fechaSeleccionada.Year >= cuotaAño) || (fechaSeleccionada.Month > cuotaMes && fechaSeleccionada.Year > cuotaAño))
            {
                FormDatosDePagoDeCuota formDatosDePagoDeCuota = new FormDatosDePagoDeCuota(cuota, alumno, fechaSeleccionada, 2);
                formDatosDePagoDeCuota.Owner = this.Owner;
                formDatosDePagoDeCuota.FormBorderStyle = FormBorderStyle.None;
                formDatosDePagoDeCuota.TopLevel = false;
                formDatosDePagoDeCuota.Dock = DockStyle.Fill;
                this.Owner.Controls.Add(formDatosDePagoDeCuota);
                this.Owner.Tag = formDatosDePagoDeCuota;
                formDatosDePagoDeCuota.BringToFront();
                formDatosDePagoDeCuota.Show();
                this.Close();
            }
            else
            {
                FormDatosDePagoDeCuota formDatosDePagoDeCuota = new FormDatosDePagoDeCuota(cuota, alumno, fechaSeleccionada, 1);
                formDatosDePagoDeCuota.Owner = this.Owner;
                formDatosDePagoDeCuota.FormBorderStyle = FormBorderStyle.None;
                formDatosDePagoDeCuota.TopLevel = false;
                formDatosDePagoDeCuota.Dock = DockStyle.Fill;
                this.Owner.Controls.Add(formDatosDePagoDeCuota);
                this.Owner.Tag = formDatosDePagoDeCuota;
                formDatosDePagoDeCuota.BringToFront();
                formDatosDePagoDeCuota.Show();
                this.Close();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
