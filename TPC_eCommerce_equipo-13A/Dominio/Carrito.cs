using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {
        public int Id { get; set; }                         //PK
        public Usuario Usuario { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public int MontoTotal { get; set; }
        public int FechaHoraCreacion { get; set; }
    }
}
