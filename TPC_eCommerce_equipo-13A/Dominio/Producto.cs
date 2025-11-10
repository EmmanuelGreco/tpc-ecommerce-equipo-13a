using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }                         //PK
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Origen { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<ProductoImagen> ListaImagen { get; set; }
        public bool Activo { get; set; }
    }
}