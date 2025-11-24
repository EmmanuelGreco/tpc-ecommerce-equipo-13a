using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }                               //PK
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}