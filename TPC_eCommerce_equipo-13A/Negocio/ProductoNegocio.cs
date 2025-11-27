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
        public List<Producto> listar(int filtro = 2)
        {
            List<Producto> listaProducto = new List<Producto>();
            ProductoImagenNegocio productoImagenNegocio = new ProductoImagenNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, P.Stock, P.Activo, M.Nombre Marca, " +
                                     "P.IdCategoria, C.Nombre Categoria, P.Origen, Precio " +
                                     "FROM PRODUCTOS P, MARCAS M, CATEGORIAS C " +
                                     "WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria";

                //0 inactivos, 1 activos, 2 (o "n") ambos
                if (filtro == 0)
                    consulta += " AND P.Activo = 0";
                else if (filtro == 1)
                    consulta += " AND P.Activo = 1";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

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

        public int agregar(string codigo, string nombre, string descripcion, int idMarca, int idCategoria, string origen, int stock, decimal precio)
        {
            AccesoDatos datos = new AccesoDatos();
            int idInsertado = 0;

            try
            {
                datos.setearConsulta(@"INSERT INTO PRODUCTOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Origen, Stock, Precio ) 
                                       VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Origen, @Stock, @Precio);
                                       SELECT CAST(SCOPE_IDENTITY() AS INT) AS 'IdInsertado';");
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdMarca", idMarca);
                datos.setearParametro("@IdCategoria", idCategoria);
                datos.setearParametro("@Origen", origen);
                datos.setearParametro("@Stock", stock);
                datos.setearParametro("@Precio", precio);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    idInsertado = (int)datos.Lector["IdInsertado"];
                }
                return idInsertado;
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

        public void modificar(int id, string codigo, string nombre, string descripcion, int idMarca, int idCategoria, string origen, int stock, decimal precio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE PRODUCTOS SET Codigo = @Codigo, 
                                                            Nombre = @Nombre, 
                                                            Descripcion = @Descripcion, 
                                                            IdMarca = @IdMarca, 
                                                            IdCategoria = @IdCategoria, 
                                                            Origen = @Origen, 
                                                            Stock = @Stock,
                                                            Precio = @Precio
                                        WHERE Id = @Id;");
                datos.setearParametro("@Id", id);
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdMarca", idMarca);
                datos.setearParametro("@IdCategoria", idCategoria);
                datos.setearParametro("@Origen", origen);
                datos.setearParametro("@Stock", stock);
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

        public Producto buscarPorId(int idProducto)
        {
            Producto producto = new Producto();
            ProductoImagenNegocio productoImagenNegocio = new ProductoImagenNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.IdMarca, P.Stock, P.Activo, M.Nombre Marca, " +
                                     "P.IdCategoria, C.Nombre Categoria, P.Origen, Precio " +
                                     "FROM PRODUCTOS P, MARCAS M, CATEGORIAS C " +
                                     "WHERE M.Id = P.IdMarca AND C.Id = P.IdCategoria AND P.Id = @IdProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    producto.Id = (int)datos.Lector["Id"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    producto.Stock = (int)datos.Lector["Stock"];
                    producto.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    producto.Marca = new Marca();
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Nombre = (string)datos.Lector["Marca"];

                    producto.Categoria = new Categoria();
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Nombre = (string)datos.Lector["Categoria"];

                    producto.Origen = (string)datos.Lector["Origen"];

                    producto.Precio = (decimal)datos.Lector["Precio"];

                    // OPCIONAL: (redondea los decimales a 2 nums despues de la coma)
                    producto.Precio = Math.Round(producto.Precio, 2, MidpointRounding.AwayFromZero);

                    //Agregar lista de imagenes
                    List<ProductoImagen> listaImagen = new List<ProductoImagen>();
                    producto.ListaImagen = productoImagenNegocio.listarPorIdProducto(producto.Id);
                }
                return producto;
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

        public void alternarEstado(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = @IdProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarAccion();
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
        
        public void actualizarStock(int idProducto, int nuevoStock)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET Stock = @Stock WHERE Id = @IdProducto");
                datos.setearParametro("@Stock", nuevoStock);
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarAccion();
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
