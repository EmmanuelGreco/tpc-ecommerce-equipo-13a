using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, TipoUsuario FROM USUARIOS WHERE Email = @email AND Contrasenia = @contrasenia");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@contrasenia", usuario.Contrasenia);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];

                    int tipo = Convert.ToInt32(datos.Lector["TipoUsuario"]);
                    usuario.TipoUsuario =
                        tipo == 0 ? UserType.CLIENTE :
                        tipo == 1 ? UserType.EMPLEADO :
                        UserType.ADMIN;

                    return true;
                }
                return false;

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
