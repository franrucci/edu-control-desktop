﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoDeAsistencia
    {
        public string Codigo {  get; set; }
        public string Nombre {  get; set; }
        public List<Asistencia> Asistencias { get; set; }

        //----------------------------------------
        public int TipoDeAsistenciaId {  get; set; }
        //----------------------------------------
    }
}
