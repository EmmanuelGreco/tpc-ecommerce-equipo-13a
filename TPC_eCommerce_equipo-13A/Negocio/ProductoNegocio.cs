using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> listaProducto = new List<Producto>();
            ProductoImagenNegocio productoImagenNegocio = new ProductoImagenNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, M.Nombre Marca, " +
                                     "P.IdCategoria, C.Nombre Categoria, P.Origen, Precio " +
                                     "FROM PRODUCTOS P, MARCAS M, CATEGORIAS C " +
                                     "WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Nombre = (string)datos.Lector["Categoria"];

                    aux.Origen = (string)datos.Lector["Origen"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    // OPCIONAL: (redondea los decimales a 2 nums despues de la coma)
                    aux.Precio = Math.Round(aux.Precio, 2, MidpointRounding.AwayFromZero);

                    //Agregar lista de imagenes
                    List<ProductoImagen> listaImagen = new List<ProductoImagen>();
                    aux.ListaImagen = productoImagenNegocio.listarPorIdProducto(aux.Id);

                    listaProducto.Add(aux);
                }
                return listaProducto;
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

        public void agregar(string codigo, string nombre, string descripcion, int idMarca, int idCategoria, string origen, decimal precio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {


                datos.setearConsulta(@"INSERT INTO PRODUCTOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Origen, Precio ) 
                                       VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Origen, @Precio);");
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdMarca", idMarca);
                datos.setearParametro("@IdCategoria", idCategoria);
                datos.setearParametro("@Origen", origen);
                datos.setearParametro("@Precio", precio);
                datos.ejecutarLectura();
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
