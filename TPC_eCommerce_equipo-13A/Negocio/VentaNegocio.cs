using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public int agregar(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            int idInsertado = 0;

            try
            {
                datos.setearConsulta(@"INSERT INTO VENTAS (IdUsuario, MetodoPago, MetodoEnvio, FechaHoraVenta, FechaHoraEntrega, EstadoPedido, MontoTotal)
                                    VALUES (@idUsuario, @metodoPago, @metodoEnvio, @fechaHoraVenta, @fechaHoraEntrega, @estadoPedido, @montoTotal);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT) AS IdInsertado;");

                if (venta.Usuario == null)
                    throw new Exception("La venta debe tener un usuario asociado.");

                datos.setearParametro("@idUsuario", venta.Usuario.Id);

                datos.setearParametro("@metodoPago", (int)venta.MetodoPago);
                datos.setearParametro("@metodoEnvio", (int)venta.MetodoEnvio);

                datos.setearParametro("@fechaHoraVenta", venta.FechaHoraVenta);

                if (venta.FechaHoraEntrega.HasValue)
                    datos.setearParametro("@fechaHoraEntrega", venta.FechaHoraEntrega.Value);
                else
                    datos.setearParametro("@fechaHoraEntrega", DBNull.Value);

                datos.setearParametro("@estadoPedido", (int)venta.EstadoPedido);

                datos.setearParametro("@montoTotal", venta.MontoTotal);

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

        public List<Venta> listarPorUsuario(int idUsuario)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT Id, IdUsuario, MetodoPago, MetodoEnvio, FechaHoraVenta, FechaHoraEntrega, EstadoPedido, MontoTotal
                                    FROM VENTAS
                                    WHERE IdUsuario = @idUsuario
                                    ORDER BY FechaHoraVenta DESC");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];

                    venta.Usuario = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"]
                    };

                    venta.MetodoPago = (PaymentMethod)Convert.ToInt32(datos.Lector["MetodoPago"]);
                    venta.MetodoEnvio = (ShippingMethod)Convert.ToInt32(datos.Lector["MetodoEnvio"]);

                    venta.FechaHoraVenta = (DateTime)datos.Lector["FechaHoraVenta"];

                    if (!(datos.Lector["FechaHoraEntrega"] is DBNull))
                        venta.FechaHoraEntrega = (DateTime)datos.Lector["FechaHoraEntrega"];

                    venta.EstadoPedido = (OrderStatus)Convert.ToInt32(datos.Lector["EstadoPedido"]);

                    venta.MontoTotal = (decimal)datos.Lector["MontoTotal"];

                    lista.Add(venta);
                }

                return lista;
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

        public void agregarProductosDeVenta(int idVenta, List<Producto> productos)
        {
            foreach (var prod in productos)
            {

                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta(@"INSERT INTO VENTAS_PRODUCTOS (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                                            VALUES (@idVenta, @idProducto, @cantidad, @precioUnitario)");

                        datos.limpiarParametros();

                        datos.setearParametro("@idVenta", idVenta);
                        datos.setearParametro("@idProducto", prod.Id);
                        datos.setearParametro("@cantidad", prod.Stock);
                        datos.setearParametro("@precioUnitario", prod.Precio);

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

        public Venta obtenerPorId(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            Venta venta = null;

            try
            {
                datos.setearConsulta(@"SELECT V.Id, V.IdUsuario, V.MetodoPago, V.MetodoEnvio,
                                           V.FechaHoraVenta, V.FechaHoraEntrega, V.EstadoPedido, V.MontoTotal,
                                           U.Nombre, U.Apellido, U.Email
                                    FROM VENTAS V
                                    INNER JOIN USUARIOS U ON U.Id = V.IdUsuario
                                    WHERE V.Id = @idVenta");

                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];

                    venta.Usuario = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Email = datos.Lector["Email"].ToString()
                    };

                    venta.MetodoPago = (PaymentMethod)Convert.ToInt32(datos.Lector["MetodoPago"]);
                    venta.MetodoEnvio = (ShippingMethod)Convert.ToInt32(datos.Lector["MetodoEnvio"]);

                    venta.FechaHoraVenta = (DateTime)datos.Lector["FechaHoraVenta"];

                    if (!(datos.Lector["FechaHoraEntrega"] is DBNull))
                        venta.FechaHoraEntrega = (DateTime)datos.Lector["FechaHoraEntrega"];

                    venta.EstadoPedido = (OrderStatus)Convert.ToInt32(datos.Lector["EstadoPedido"]);

                    venta.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                }

                return venta;
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

        public List<Producto> listarProductosPorVenta(int idVenta)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT VP.IdProducto, VP.Cantidad, VP.PrecioUnitario,
                                           P.Nombre, P.Descripcion
                                    FROM VENTAS_PRODUCTOS VP
                                    INNER JOIN PRODUCTOS P ON P.Id = VP.IdProducto
                                    WHERE VP.IdVenta = @idVenta");

                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.Id = (int)datos.Lector["IdProducto"];
                    prod.Nombre = datos.Lector["Nombre"].ToString();
                    prod.Descripcion = datos.Lector["Descripcion"].ToString();

                    prod.Precio = (decimal)datos.Lector["PrecioUnitario"];

                    prod.Stock = (int)datos.Lector["Cantidad"];

                    lista.Add(prod);
                }

                return lista;
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

        public void ActualizarEstadoPedido(int idVenta, OrderStatus nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE VENTAS SET EstadoPedido = @estado WHERE Id = @idVenta");
                datos.setearParametro("@estado", (int)nuevoEstado);
                datos.setearParametro("@idVenta", idVenta);

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
