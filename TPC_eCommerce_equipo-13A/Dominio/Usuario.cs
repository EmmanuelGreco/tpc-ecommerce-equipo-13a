using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string Email { get; set; }                         //PK
        public string Contrasenia { get; set; }
        public Persona Persona { get; set; }
        public string FechaAlta { get; set; }
    }
}