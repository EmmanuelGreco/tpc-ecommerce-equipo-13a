using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ProductoImagen
    {
        public int Id { get; set; }                         //PK
        public int IdProducto { get; set; }
        public string ImagenUrl { get; set; }
    }
}
