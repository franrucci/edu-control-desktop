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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace Vista
{
    public partial class FormRolesUsuarios : Form
    {
        private bool EsNuevoRol;
        private int? RolId;
        public FormRolesUsuarios()
        {
            InitializeComponent();
            EsNuevoRol = true;
            RolId = null;
        }

        public FormRolesUsuarios(int rolId)
        {
            InitializeComponent();
            EsNuevoRol = false;
            RolId = rolId;
        }

        private void CargarRol(int rolId)
        {
            var permisos = ControladoraPermisos.Instancia.ObtenerPermisosPorRol(rolId);

            // Marca los checkboxes correspondientes
            foreach (Control control in gBoxControles.Controls)
            {
                if (control is CheckBox chkBox)
                {
                    var opcionId = Convert.ToInt32(chkBox.Tag);
                    var permiso = permisos.FirstOrDefault(p => p.OpcionId == opcionId);

                    if (permiso != null)
                    {
                        chkBox.Checked = permiso.Permitido; // Marca según el permiso
                    }
                }
            }

            var rol = ControladoraRoles.Instancia.ObtenerRolPorId(rolId);
            if (rol != null)
            {
                txtUsuario.Text = rol.RolNombre;
            }
        }

        private void CheckRol()
        {
            int rolId = ControladoraRoles.Instancia.ObtenerIdPorNombre(txtUsuario.Text);

            // Elimina los permisos anteriores del rol
            ControladoraPermisos.Instancia.EliminarPermisosPorRol(rolId);

            // Recorre los CheckBox y guarda los permisos actualizados
            foreach (Control control in gBoxControles.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Tag != null)
                {
                    if (int.TryParse(checkBox.Tag.ToString(), out int opcionId))
                    {
                        Permiso permisoEntidad = new Permiso
                        {
                            RolUsuarioId = rolId,
                            OpcionId = opcionId,
                            Permitido = checkBox.Checked
                        };

                        ControladoraPermisos.Instancia.GuardarPermiso(permisoEntidad);
                    }
                }
            }
        }

        private void Limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtUsuario.Focus();

            foreach (Control chk in gBoxControles.Controls)
            {
                if (chk is System.Windows.Forms.CheckBox)
                {
                    // Desmarca los CheckBox
                    if (((System.Windows.Forms.CheckBox)chk).Checked)
                        ((System.Windows.Forms.CheckBox)chk).Checked = false;
                }
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el rol.");
                return false;
            }

            bool nombreRepetido = ControladoraRoles.Instancia.ValidarNombre(txtUsuario.Text, RolId);
            if (nombreRepetido)
            {
                MessageBox.Show("El nombre del rol ya existe.");
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (RolId.HasValue)  // Si estamos editando un rol
                {
                    ControladoraRoles.Instancia.ModificarRol(RolId.Value, txtUsuario.Text);
                }
                else
                {
                    // Sino es agregar un nuevo rol
                    RolId = ControladoraRoles.Instancia.GuardarRol(txtUsuario.Text);
                }

                CheckRol();

                Limpiar();

                MessageBox.Show("Rol de usuario guardado");
            }
        }


        //private void GuardarPermisos()
        //{
        //    if (RolId.HasValue)
        //    {
        //        ControladoraPermisos.Instancia.EliminarPermisosPorRol(RolId.Value);
        //    }

        //    foreach (Control control in gBoxControles.Controls)
        //    {
        //        if (control is CheckBox chkBox)
        //        {
        //            var permiso = new Permiso
        //            {
        //                RolUsuarioId = RolId.Value,
        //                OpcionId = Convert.ToInt32(chkBox.Tag),
        //                Permitido = chkBox.Checked
        //            };

        //            ControladoraPermisos.Instancia.GuardarPermiso(permiso);
        //        }
        //    }
        //}

        private void chBoxEnviarEmail_CheckedChanged(object sender, EventArgs e)
        {
            chBoxEnviarEmail1.Checked = chBoxEnviarEmail.Checked;
        }

        private void chBoxEnviarEmail1_CheckedChanged(object sender, EventArgs e)
        {
            chBoxEnviarEmail.Checked = chBoxEnviarEmail1.Checked;
        }

        private void chBoxDatosPersonales_CheckedChanged(object sender, EventArgs e)
        {
            chBoxDatosPersonales1.Checked = chBoxDatosPersonales.Checked;
        }

        private void chBoxDatosPersonales1_CheckedChanged(object sender, EventArgs e)
        {
            chBoxDatosPersonales.Checked = chBoxDatosPersonales1.Checked;
        }

        private void chBoxInformacionAcademica_CheckedChanged(object sender, EventArgs e)
        {
            chBoxInformacionAcademica1.Checked = chBoxInformacionAcademica.Checked;
        }

        private void chBoxInformacionAcademica1_CheckedChanged(object sender, EventArgs e)
        {
            chBoxInformacionAcademica.Checked = chBoxInformacionAcademica1.Checked;
        }

        private void gBoxControles_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(gBoxControles.BackColor);
        }

        private void FormRolesUsuarios_Load(object sender, EventArgs e)
        {
            if (!EsNuevoRol && RolId.HasValue)
            {
                CargarRol(RolId.Value);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
