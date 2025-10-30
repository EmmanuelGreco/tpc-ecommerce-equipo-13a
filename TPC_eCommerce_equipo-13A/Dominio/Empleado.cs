using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado
    {
        public int Legajo { get; set; }                         //PK
        public Persona Persona { get; set; }
        public string Cargo { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaFin { get; set; }
    }
}