using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public int Id { get; set; }                             //PK
        public Usuario Usuario { get; set; }
        public bool Activo { get; set; }
    }
}
