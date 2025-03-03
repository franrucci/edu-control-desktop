using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AlumnoAuditoria
    {
        [Key]
        public int AlumnoAuditoriaId { get; set; }

        public Alumno? Alumno { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;

        [Required]
        public string Accion { get; set; }

        public string? DatosAnteriores { get; set; }

        public string? DatosNuevos { get; set; }

        //----------------------------------------
        public int? PersonaId { get; set; }

        public int UsuarioId { get; set; }
        //----------------------------------------
    }
}
