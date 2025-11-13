using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> listaCliente = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT   C.Id, 
                                                U.Documento, 
                                                U.Nombre, 
                                                U.Apellido, 
                                                U.FechaNacimiento, 
                                                U.Telefono, 
                                                U.Direccion, 
                                                U.CodigoPostal, 
                                                U.Email, 
                                                U.Contrasenia, 
                                                U.FechaAlta, 
                                                C.Activo 
                                        FROM Clientes C INNER JOIN Usuarios U ON C.IdUsuario = U.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Usuario = new Usuario();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Usuario.Documento = (int)datos.Lector["Documento"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Usuario.Telefono = (int)datos.Lector["Telefono"];
                    aux.Usuario.Direccion = (string)datos.Lector["Direccion"];
                    aux.Usuario.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Contrasenia = (string)datos.Lector["Contrasenia"];
                    aux.Usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    listaCliente.Add(aux);
                }
                return listaCliente;
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

        public Cliente buscarPorId(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT   C.Id, 
                                                U.Documento, 
                                                U.Nombre, 
                                                U.Apellido, 
                                                U.FechaNacimiento, 
                                                U.Telefono, 
                                                U.Direccion, 
                                                U.CodigoPostal, 
                                                U.Email, 
                                                U.Contrasenia, 
                                                U.FechaAlta, 
                                                C.Activo 
                                        FROM Clientes C INNER JOIN Usuarios U ON C.IdUsuario = U.Id
                                        WHERE C.Id = @IdCliente");
                datos.setearParametro("@IdCliente", idCliente);
                datos.ejecutarLectura();

                Cliente aux = new Cliente();
                aux.Usuario = new Usuario();

                while (datos.Lector.Read())
                {

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Usuario.Documento = (int)datos.Lector["Documento"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Usuario.Telefono = (int)datos.Lector["Telefono"];
                    aux.Usuario.Direccion = (string)datos.Lector["Direccion"];
                    aux.Usuario.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Contrasenia = (string)datos.Lector["Contrasenia"];
                    aux.Usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                }
                return aux;
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
        public void modificar(int idCliente, int documento, string nombre, string apellido, DateTime fechaNacimiento, int telefono,
                            string direccion, string codigoPostal, string email, string contrasenia, DateTime fechaAlta)
        {
            AccesoDatos datos = new AccesoDatos();
            int idUsuario = 0;
            try
            {
                datos.setearConsulta(@"SELECT IdUsuario FROM CLIENTES WHERE Id = @IdCliente");
                datos.setearParametro("@IdCliente", idCliente);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                    idUsuario = (int)datos.Lector["IdUsuario"];
                datos.cerrarConexion();

                datos.setearConsulta(@"UPDATE USUARIOS SET 
                                        Documento = @Documento, 
                                        Nombre = @Nombre, 
                                        Apellido = @Apellido, 
                                        FechaNacimiento = @FechaNacimiento, 
                                        Telefono = @Telefono, 
                                        Direccion = @Direccion, 
                                        CodigoPostal = @CodigoPostal, 
                                        Email = @Email, 
                                        Contrasenia = @Contrasenia, 
                                        FechaAlta = @FechaAlta 
                                     WHERE Id = @idUsuario");
                datos.setearParametro("@Documento", documento);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@FechaNacimiento", fechaNacimiento);
                datos.setearParametro("@Telefono", telefono);
                datos.setearParametro("@Direccion", direccion);
                datos.setearParametro("@CodigoPostal", codigoPostal);
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Contrasenia", contrasenia);
                datos.setearParametro("@FechaAlta", fechaAlta);
                datos.setearParametro("@idUsuario", idUsuario);
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
        public void agregar(int documento, string nombre, string apellido, DateTime fechaNacimiento, int telefono,
                            string direccion, string codigoPostal, string email, string contrasenia, DateTime fechaAlta)
        {
            AccesoDatos datos = new AccesoDatos();
            int idInsertado = 0;
            try
            {
                datos.setearConsulta(@"INSERT INTO USUARIOS (Documento, Nombre, Apellido, FechaNacimiento, Telefono, Direccion, CodigoPostal, Email, Contrasenia, FechaAlta) 
                                       VALUES (@Documento, @Nombre, @Apellido, @FechaNacimiento, @Telefono, @Direccion, @CodigoPostal, @Email, @Contrasenia, @FechaAlta);
                                       SELECT CAST(SCOPE_IDENTITY() AS INT) AS 'IdInsertado';");
                datos.setearParametro("@Documento", documento);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@FechaNacimiento", fechaNacimiento);
                datos.setearParametro("@Telefono", telefono);
                datos.setearParametro("@Direccion", direccion);
                datos.setearParametro("@CodigoPostal", codigoPostal);
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Contrasenia", contrasenia);
                datos.setearParametro("@FechaAlta", fechaAlta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    idInsertado = (int)datos.Lector["IdInsertado"];
                datos.cerrarConexion();

                datos.setearConsulta(@"INSERT INTO CLIENTES (IdUsuario) 
                                       VALUES (@IdInsertado);");
                datos.setearParametro("@IdInsertado", idInsertado);
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
        public void alternarEstado(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE CLIENTES SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = @IdCliente");
                datos.setearParametro("@IdCliente", idCliente);
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
