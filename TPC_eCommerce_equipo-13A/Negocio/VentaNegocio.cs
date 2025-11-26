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
                datos.setearConsulta(@"
                    INSERT INTO VENTAS (IdUsuario, MetodoPago, MetodoEnvio, FechaHoraVenta, FechaHoraEntrega, MontoTotal)
                    VALUES (@idUsuario, @metodoPago, @metodoEnvio, @fechaHoraVenta, @fechaHoraEntrega, @montoTotal);
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
    }
}
