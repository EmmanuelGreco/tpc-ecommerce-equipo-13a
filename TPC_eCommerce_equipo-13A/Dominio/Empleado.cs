using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado
    {
        public int Id { get; set; }                             //PK
        public Usuario Usuario { get; set; }
        public int Legajo { get; set; }
        public string Cargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaDespido { get; set; }
        public bool Activo { get; set; }
    }
}