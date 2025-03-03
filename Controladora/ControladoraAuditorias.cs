using Entidades;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Controladora
{
    public class ControladoraAuditorias
    {
        private SistemaColegio sistemaColegio;
        private static ControladoraAuditorias instancia;

        public static ControladoraAuditorias Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ControladoraAuditorias();
                }
                return instancia;
            }
        }
        ControladoraAuditorias()
        {
            sistemaColegio = new SistemaColegio();
        }

        public void RegistrarAuditoria(Alumno alumno, Usuario usuario, string accion, object? datosAnteriores, object? datosNuevos)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Ignora las referencias cíclicas
                };

                var auditoria = new AlumnoAuditoria
                {
                    PersonaId = alumno.PersonaId,
                    UsuarioId = usuario.UsuarioId,
                    FechaHora = DateTime.Now,
                    Accion = accion,
                    DatosAnteriores = datosAnteriores != null ? JsonConvert.SerializeObject(datosAnteriores, settings) : null,
                    DatosNuevos = datosNuevos != null ? JsonConvert.SerializeObject(datosNuevos, settings) : null
                };

                sistemaColegio.AlumnosAuditoria.Add(auditoria);
                sistemaColegio.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar auditoría: {ex.Message}");
                throw;
            }
        }

        public List<AlumnoAuditoria> ObtenerAuditorias()
        {
            try
            {
                return sistemaColegio.AlumnosAuditoria
                .OrderByDescending(a => a.FechaHora)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener auditorías: {ex.Message}");
                throw;
            }
        }

        public void RegistrarLoginLogoutAuditoria(Usuario usuario, string accion)
        {
            try
            {
                var auditoria = new LoginLogoutAuditoria
                {
                    UsuarioId = usuario.UsuarioId,
                    FechaHora = DateTime.Now,
                    Accion = accion
                };

                sistemaColegio.LoginLogoutAuditorias.Add(auditoria);
                sistemaColegio.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar auditoría de login/logout: {ex.Message}");
                throw;
            }
        }

        public List<LoginLogoutAuditoria> ObtenerLoginLogoutAuditorias()
        {
            try
            {
                return sistemaColegio.LoginLogoutAuditorias
                .OrderByDescending(a => a.FechaHora)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener auditorías de login/logout: {ex.Message}");
                throw;
            }
        }
    }
}
