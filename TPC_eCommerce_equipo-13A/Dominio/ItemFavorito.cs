using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ItemFavorito
    {
        public int Id { get; set; }                         //PK
        public Producto Producto { get; set; }
        public string FechaAgregado { get; set; }
    }
}
