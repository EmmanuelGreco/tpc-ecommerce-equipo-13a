using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoImagenNegocio
    {
        public List<ProductoImagen> listarPorIdProducto(int IdProducto)
        {
            List<ProductoImagen> listaProductoImagen = new List<ProductoImagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdProducto, ImagenUrl " +
                                     "FROM IMAGENES WHERE IdProducto = " + IdProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ProductoImagen aux = new ProductoImagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdProducto = (int)datos.Lector["IdProducto"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    listaProductoImagen.Add(aux);
                }
                return listaProductoImagen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}