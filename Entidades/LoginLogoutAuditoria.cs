using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LoginLogoutAuditoria
    {
        [Key]
        public int LoginLogoutAuditoriaId { get; set; }
        public Usuario? Usuario { get; set; }

        public DateTime FechaHora { get; set; }

        [MaxLength(10)]
        public string Accion { get; set; }

        //----------------------------------------
        public int UsuarioId { get; set; }
        //----------------------------------------
    }
}
