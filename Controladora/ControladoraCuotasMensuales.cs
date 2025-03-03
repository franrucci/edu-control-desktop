using Entidades;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class ControladoraCuotasMensuales
    {
        private SistemaColegio sistemaColegio;
        private static ControladoraCuotasMensuales instancia;

        public static ControladoraCuotasMensuales Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ControladoraCuotasMensuales();
                }
                return instancia;
            }
        }

        ControladoraCuotasMensuales()
        {
            sistemaColegio = new SistemaColegio();
        }

        public ReadOnlyCollection<Cuota> ListarCuotasPorAño(CicloAcademico ciclo)
        {
            try
            {
                return sistemaColegio.Cuotas
                    .Where(c => c.CicloAcademicoId == ciclo.CicloAcademicoId)
                    .ToList()
                    .AsReadOnly();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string RegistrarCuota(Cuota cuota)
        {
            try
            {
                // Valida si ya existe una cuota en el mismo año del ciclo académico y mes
                var ciclo = sistemaColegio.CiclosAcademicos
                    .FirstOrDefault(c => c.CicloAcademicoId == cuota.CicloAcademico.CicloAcademicoId);

                if (ciclo == null)
                {
                    return $"Error: No se encontró el ciclo académico con ID {cuota.CicloAcademicoId}.";
                }

                var cuotaExistente = sistemaColegio.Cuotas
                    .FirstOrDefault(c => c.CicloAcademicoId == cuota.CicloAcademico.CicloAcademicoId && c.Mes == cuota.Mes);

                if (cuotaExistente != null)
                {
                    return $"Error: Ya existe una cuota registrada para el ciclo académico {ciclo.Año} en el mes {cuota.Mes}.";
                }

                // Valida si el mes anterior existe para el mismo ciclo académico
                if (cuota.Mes > 1)
                {
                    var cuotaMesAnterior = sistemaColegio.Cuotas
                        .FirstOrDefault(c => c.CicloAcademicoId == cuota.CicloAcademico.CicloAcademicoId && c.Mes == cuota.Mes - 1);

                    if (cuotaMesAnterior == null)
                    {
                        return $"Error: No se puede registrar la cuota para el mes {cuota.Mes} del ciclo académico {ciclo.Año} porque falta registrar la cuota del mes {cuota.Mes - 1}.";
                    }
                }

                cuota.CicloAcademico = ciclo;

                sistemaColegio.Cuotas.Add(cuota);
                sistemaColegio.SaveChanges();

                return "Cuota registrada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Ocurrió un error al intentar registrar la cuota: {ex.Message}";
            }
        }

        public string ModificarCuota(Cuota cuota)
        {
            try
            {
                var cuotaExistente = sistemaColegio.Cuotas.FirstOrDefault(c => c.CicloAcademicoId == cuota.CicloAcademico.CicloAcademicoId && c.Mes == cuota.Mes);

                if (cuotaExistente == null)
                {
                    return $"Error: La cuota a modificar no se encuentra registrada.";
                }
                else
                {
                    cuotaExistente.Precio = cuota.Precio;
                    sistemaColegio.SaveChanges();
                    return "Cuota modificada exitosamente.";
                }
            }
            catch (Exception ex)
            {
                return $"Ocurrió un error al intentar modificar la cuota: {ex.Message}";
            }
        }

        public string EliminarCuota(Cuota cuota, CicloAcademico ciclo)
        {
            try
            {
                // Validar si existe la cuota indicada
                var cuotaExistente = sistemaColegio.Cuotas
                    .FirstOrDefault(c => c.CuotaId == cuota.CuotaId && c.CicloAcademicoId == ciclo.CicloAcademicoId);

                if (cuotaExistente == null)
                {
                    return $"Error: No se encontró la cuota a eliminar.";
                }

                // Validar si el mes de la cuota es menor a 12
                if (cuotaExistente.Mes < 12)
                {
                    // Buscar si existe una cuota con el mismo ciclo académico y el mes posterior
                    var cuotaMesPosterior = sistemaColegio.Cuotas
                        .FirstOrDefault(c => c.CicloAcademicoId == ciclo.CicloAcademicoId && c.Mes == cuotaExistente.Mes + 1);

                    if (cuotaMesPosterior != null)
                    {
                        return $"Error: No se puede eliminar la cuota del mes {cuotaExistente.Mes} del ciclo académico {ciclo.Año} porque existe una cuota registrada para el mes {cuotaExistente.Mes + 1}. Primero debe eliminar la cuota con mes posterior.";
                    }
                }

                sistemaColegio.Cuotas.Remove(cuotaExistente);
                sistemaColegio.SaveChanges();

                return "Cuota eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Ocurrió un error al intentar eliminar la cuota: {ex.Message}";
            }
        }

        public List<Cuota> ObtenerCuotasImpagas(Alumno alumno)
        {
            try
            {
                // Obtiene los años de los boletines del alumno
                var añosBoletines = sistemaColegio.Boletines
                    .Where(b => b.PersonaId == alumno.PersonaId)
                    .Select(b => b.Año)
                    .Distinct()
                    .ToList();

                // Busca todas las cuotas correspondientes a esos años que no han sido pagas
                var cuotasImpagas = sistemaColegio.Cuotas
                    .Include(c => c.CicloAcademico)
                    .Where(c => añosBoletines.Contains(c.CicloAcademico.Año) && !sistemaColegio.PagosDeCuota.Any(pc => pc.CuotaId == c.CuotaId && pc.AlumnoId == alumno.PersonaId && pc.EstadoCuota.EstadoCuotaId == 1))
                    .ToList();

                return cuotasImpagas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al obtener las cuotas impagas: {ex.Message}");
                return new List<Cuota>();
            }
        }

        public List<Cuota> ObtenerCuotasPagas(Alumno alumno)
        {
            try
            {
                var añosBoletines = sistemaColegio.Boletines
                    .Where(b => b.PersonaId == alumno.PersonaId)
                    .Select(b => b.Año)
                    .Distinct()
                    .ToList();

                var cuotasPagas = sistemaColegio.Cuotas
                    .Include(c => c.CicloAcademico)
                    .Where(c => añosBoletines.Contains(c.CicloAcademico.Año) && sistemaColegio.PagosDeCuota.Any(pc => pc.CuotaId == c.CuotaId && pc.AlumnoId == alumno.PersonaId && pc.EstadoCuota.EstadoCuotaId == 1))
                    .ToList();

                return cuotasPagas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al obtener las cuotas pagas: {ex.Message}");
                return new List<Cuota>();
            }
        }

        public Cuota ObtenerCuotaPorId(int cuotaId)
        {
            try
            {
                return sistemaColegio.Cuotas.Include(c => c.CicloAcademico).FirstOrDefault(c => c.CuotaId == cuotaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al obtener la cuota: {ex.Message}");
                return null;
            }
        }

        public PagoDeCuota ObtenerPagoDeCuotaPorCuotaId(int cuotaId, int alumnoId)
        {
            try
            {
                return sistemaColegio.PagosDeCuota
                    .Include(p => p.Alumno)
                    .Include(p => p.Cuota)
                    .Include(p => p.Cuota.CicloAcademico)
                    .Include(p => p.EstadoCuota)
                    .Include(p => p.Pago)
                    .Include(p => p.Pago.TipoDePago)
                    .FirstOrDefault(p => p.CuotaId == cuotaId && p.AlumnoId == alumnoId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al obtener el pago de cuota: {ex.Message}");
                return null;
            }
        }
    }
}
