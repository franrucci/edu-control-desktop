using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Patron_Strategy
{
    public interface IAlumnoStrategy
    {
        string AgregarAlumno(Alumno alumno, CicloAcademico cicloAcademico, GradoAcademico grado, int IdUsu);
    }
}