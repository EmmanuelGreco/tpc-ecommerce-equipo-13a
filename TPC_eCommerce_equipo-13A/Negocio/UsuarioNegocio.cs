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
        public int Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT e.Activo AS 'Empleado', c.Activo AS 'Cliente' FROM Usuarios u
	                                        LEFT JOIN Clientes c ON u.Id = c.IdUsuario
	                                        LEFT JOIN Empleados e ON u.Id = e.IdUsuario
                                        WHERE u.Email = @email AND u.Contrasenia = @contrasenia");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@contrasenia", usuario.Contrasenia);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string empleado = datos.Lector["Empleado"].ToString();
                    string cliente = datos.Lector["Cliente"].ToString();

                    if (empleado == "True" || cliente == "True")
                        return 0;
                    else if (empleado == "False" || cliente == "False")
                        return 2;
                }
                return 1;

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

        public Usuario obtenerPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setearConsulta("SELECT Id, Email FROM USUARIOS WHERE Id = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Email = datos.Lector["Email"].ToString();
                }

                return usuario;
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


        public Usuario ObtenerDatos(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuarioLogueado = new Usuario();
            try
            {
                datos.setearConsulta(@"SELECT Id, Documento, Nombre, Apellido, FechaNacimiento, Telefono, Direccion, CodigoPostal, Email, Contrasenia, TipoUsuario, FechaAlta 
                                        FROM USUARIOS WHERE Email = @email");
                datos.setearParametro("@email", usuario.Email);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuarioLogueado.Id = (int)datos.Lector["Id"];
                    usuarioLogueado.Documento = datos.Lector["Documento"].ToString();
                    usuarioLogueado.Nombre = datos.Lector["Nombre"].ToString();
                    usuarioLogueado.Apellido = datos.Lector["Apellido"].ToString();
                    usuarioLogueado.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    usuarioLogueado.Telefono = (long)datos.Lector["Telefono"];
                    usuarioLogueado.Direccion = datos.Lector["Direccion"].ToString();
                    usuarioLogueado.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                    usuarioLogueado.Email = datos.Lector["Email"].ToString();
                    usuarioLogueado.Contrasenia = datos.Lector["Contrasenia"].ToString();
                    int tipo = Convert.ToInt32(datos.Lector["TipoUsuario"]);
                    usuarioLogueado.TipoUsuario =
                        tipo == 0 ? UserType.CLIENTE :
                        tipo == 1 ? UserType.EMPLEADO :
                        UserType.ADMIN;
                    usuarioLogueado.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                }

                return usuarioLogueado;

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

        public bool mailExiste(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT Email FROM Usuarios WHERE Email = @Email");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                string emailEncontrado = "";
                while (datos.Lector.Read())
                    emailEncontrado = datos.Lector["Email"].ToString();
                if (emailEncontrado == "")
                    return false;
                return true;
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

        public void cambiarContrasenia(int idUsuario, string contrasenia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Contrasenia = @Contrasenia WHERE Id = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@Contrasenia", contrasenia);
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
