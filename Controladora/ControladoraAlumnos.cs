using Entidades;
using Microsoft.EntityFrameworkCore;
using Modelo;
using Modelo.Patron_Strategy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controladora
{
    public class ControladoraAlumnos
    {
        private SistemaColegio sistemaColegio;
        private static ControladoraAlumnos instancia;
        private IAlumnoStrategy estrategia;

        public static ControladoraAlumnos Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ControladoraAlumnos();
                }
                return instancia;
            }
        }

        ControladoraAlumnos()
        {
            sistemaColegio = new SistemaColegio();
        }

        public Alumno ObtenerAlumnoPorId(int personaId)
        {
            try
            {
                return sistemaColegio.Alumnos.Include(a => a.CicloAcademico).Include(a => a.GradoAcademico).FirstOrDefault(a => a.PersonaId == personaId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CambiarEstrategia(IAlumnoStrategy nuevaEstrategia)
        {
            this.estrategia = nuevaEstrategia;
        }

        public ReadOnlyCollection<Alumno> ListarAlumnos()
        {
            try
            {
                return sistemaColegio.Alumnos.ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Agregar(Alumno alumno, CicloAcademico cicloAcademico, GradoAcademico grado, int idUsu)
        {
            try
            {
                return estrategia.AgregarAlumno(alumno, cicloAcademico, grado, idUsu);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Alumno> ObtenerAlumnosPorGrado(GradoAcademico grado, CicloAcademico ciclo)
        {
            var alumnos = sistemaColegio.Alumnos
                .Where(a => a.GradoAcademicoId == grado.GradoAcademicoId && a.CicloAcademicoId == ciclo.CicloAcademicoId)
                .ToList();

            return alumnos;
        }

        public List<Alumno> ObtenerTodosLosAlumnos()
        {
            var alumnos = sistemaColegio.Alumnos.ToList();
            return alumnos;
        }

        public string EliminarAlumno(int alumnoId, int idUsu)
        {
            try
            {
                // Obtengo el alumno antes de eliminarlo para la auditoría
                var alumnoAEliminar = sistemaColegio.Alumnos
                    .Include(a => a.GradoAcademico)
                    .Include(a => a.Sexo)
                    .Include(a => a.CicloAcademico)
                    .FirstOrDefault(a => a.PersonaId == alumnoId);

                if (alumnoAEliminar == null)
                {
                    return "No se encontró el alumno especificado.";
                }

                // Guardo los datos antes de eliminar para la auditoría
                var datosAnteriores = new
                {
                    Nombre = alumnoAEliminar.Nombre,
                    Apellido = alumnoAEliminar.Apellido,
                    Dni = alumnoAEliminar.Dni,
                    FechaDeNacimiento = alumnoAEliminar.FechaDeNacimiento,
                    Domicilio = alumnoAEliminar.Domicilio,
                    Localidad = alumnoAEliminar.Localidad,
                    CodigoPostal = alumnoAEliminar.CodigoPostal,
                    Provincia = alumnoAEliminar.Provincia,
                    Pais = alumnoAEliminar.Pais,
                    Email = alumnoAEliminar.Email,
                    Sexo = alumnoAEliminar.Sexo?.Nombre ?? "No disponible",
                    GradoAcademico = alumnoAEliminar.GradoAcademico != null
                        ? new { alumnoAEliminar.GradoAcademico.GradoAcademicoId, alumnoAEliminar.GradoAcademico.NumGrado }
                        : null,
                    CicloAcademico = alumnoAEliminar.CicloAcademico != null
                        ? new { alumnoAEliminar.CicloAcademico.Año }
                        : null
                };

                // Obtengo el usuario que realizó la eliminación
                var usuario = ControladoraUsuarios.Instancia.RetornarUsuario(idUsu);

                // Registro auditoría antes de eliminar al alumno
                ControladoraAuditorias.Instancia.RegistrarAuditoria(alumnoAEliminar, usuario, "Eliminación de alumno", datosAnteriores, null);

                // Elimino boletines relacionados
                var boletinesRelacionados = sistemaColegio.Boletines
                    .Where(b => b.PersonaId == alumnoId)
                    .ToList();
                sistemaColegio.Boletines.RemoveRange(boletinesRelacionados);
                sistemaColegio.SaveChanges();

                // Elimino pagos de cuota relacionados
                var pagosRelacionados = sistemaColegio.PagosDeCuota
                    .Where(p => p.AlumnoId == alumnoId)
                    .ToList();
                sistemaColegio.PagosDeCuota.RemoveRange(pagosRelacionados);
                sistemaColegio.SaveChanges();

                // Elimino al alumno de la tabla personas
                sistemaColegio.Personas.Remove(alumnoAEliminar);
                sistemaColegio.SaveChanges();

                return "Alumno eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al intentar eliminar al alumno: {ex.Message}";
            }
        }

        public string ModificarAlumno(Alumno alumno, int idAlumno, int idUsu)
        {
            try
            {
                // Recupero el alumno existente desde la base de datos sin el seguimiento
                var alumnoExistente = sistemaColegio.Alumnos
                    .AsNoTracking()
                    .Include(a => a.GradoAcademico)
                    .Include(a => a.Sexo)
                    .Include(a => a.CicloAcademico)
                    .FirstOrDefault(a => a.PersonaId == alumno.PersonaId);

                if (alumnoExistente == null)
                {
                    return "El alumno no fue encontrado en el sistema.";
                }

                // Clono los datos anteriores antes de modificar.
                var datosAnteriores = new
                {
                    Nombre = alumnoExistente.Nombre,
                    Apellido = alumnoExistente.Apellido,
                    Dni = alumnoExistente.Dni,
                    FechaDeNacimiento = alumnoExistente.FechaDeNacimiento,
                    Domicilio = alumnoExistente.Domicilio,
                    Localidad = alumnoExistente.Localidad,
                    CodigoPostal = alumnoExistente.CodigoPostal,
                    Provincia = alumnoExistente.Provincia,
                    Pais = alumnoExistente.Pais,
                    Email = alumnoExistente.Email,
                    Sexo = alumnoExistente.Sexo?.Nombre ?? "No disponible",
                    GradoAcademico = alumnoExistente.GradoAcademico != null
                        ? new { alumnoExistente.GradoAcademico.GradoAcademicoId, alumnoExistente.GradoAcademico.NumGrado }
                        : null,
                    CicloAcademico = alumnoExistente.CicloAcademico != null
                        ? new { alumnoExistente.CicloAcademico.Año }
                        : null
                };

                // obtengo el alumno con seguimiento para modificarlo
                var alumnoAModificar = sistemaColegio.Alumnos
                    .Include(a => a.GradoAcademico)
                    .Include(a => a.Sexo)
                    .Include(a => a.CicloAcademico)
                    .FirstOrDefault(a => a.PersonaId == alumno.PersonaId);

                if (alumnoAModificar == null)
                {
                    return "Error inesperado: No se pudo recuperar el alumno para modificar.";
                }

                // Actualizo los datos personales del alumno
                alumnoAModificar.Nombre = alumno.Nombre;
                alumnoAModificar.Apellido = alumno.Apellido;
                alumnoAModificar.Dni = alumno.Dni;
                alumnoAModificar.FechaDeNacimiento = alumno.FechaDeNacimiento;
                alumnoAModificar.Domicilio = alumno.Domicilio;
                alumnoAModificar.Localidad = alumno.Localidad;
                alumnoAModificar.CodigoPostal = alumno.CodigoPostal;
                alumnoAModificar.Provincia = alumno.Provincia;
                alumnoAModificar.Pais = alumno.Pais;
                alumnoAModificar.Email = alumno.Email;

                // manejo el Sexo que provenga del contexto para evitar conflicto de seguimiento
                if (alumno.Sexo != null)
                {
                    alumnoAModificar.Sexo = sistemaColegio.Sexos.Find(alumno.Sexo.SexoId);
                }

                // Manejo el GradoAcademico para evitar conflictos de seguimiento de instancias.
                if (alumno.GradoAcademico != null)
                {
                    alumnoAModificar.GradoAcademico = sistemaColegio.GradosAcademicos.Find(alumno.GradoAcademico.GradoAcademicoId);
                }

                sistemaColegio.SaveChanges();

                var usuario = ControladoraUsuarios.Instancia.RetornarUsuario(idUsu);

                ControladoraAuditorias.Instancia.RegistrarAuditoria(alumnoAModificar, usuario, "Modificación de alumno", datosAnteriores, alumnoAModificar);

                return "El alumno se modificó correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al intentar modificar al alumno: {ex.Message}";
            }
        }
    }
}
