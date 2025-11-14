using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmpleadoNegocio
    {
        public List<Empleado> listar()
        {
            List<Empleado> listaEmpleado = new List<Empleado>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT E.Id,
		                                E.Legajo,
		                                E.Cargo,
		                                E.FechaIngreso,
		                                E.FechaDespido,
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
		                                E.Activo 
                                      FROM Empleados E INNER JOIN Usuarios U ON E.IdUsuario = U.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado aux = new Empleado();
                    aux.Usuario = new Usuario();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Legajo = (int)datos.Lector["Legajo"];
                    aux.Cargo = (string)datos.Lector["Cargo"];
                    aux.FechaIngreso = (DateTime)datos.Lector["FechaIngreso"];
                    aux.FechaDespido = datos.Lector["FechaDespido"] != DBNull.Value ? (DateTime)datos.Lector["FechaDespido"] : (DateTime?)null;
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

                    listaEmpleado.Add(aux);
                }
                return listaEmpleado;
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

        public Empleado buscarPorId(int idEmpleado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT E.Id,
		                                E.Legajo,
		                                E.Cargo,
		                                E.FechaIngreso,
		                                E.FechaDespido,
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
		                                E.Activo 
                                      FROM Empleados E INNER JOIN Usuarios U ON E.IdUsuario = U.Id WHERE E.Id = @IdEmpleado");
                datos.setearParametro("@IdEmpleado", idEmpleado);
                datos.ejecutarLectura();

                Empleado aux = new Empleado();
                aux.Usuario = new Usuario();

                while (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Legajo = (int)datos.Lector["Legajo"];
                    aux.Cargo = (string)datos.Lector["Cargo"];
                    aux.FechaIngreso = (DateTime)datos.Lector["FechaIngreso"];
                    aux.FechaDespido = datos.Lector["FechaDespido"] != DBNull.Value ? (DateTime)datos.Lector["FechaDespido"] : (DateTime?)null;
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
        public void modificar(int idEmpleado, int legajo, string cargo, DateTime fechaIngreso, DateTime? fechaDespido,
                              int documento, string nombre, string apellido, DateTime fechaNacimiento, int telefono,
                              string direccion, string codigoPostal, string email, string contrasenia, DateTime fechaAlta)
        {
            AccesoDatos datos = new AccesoDatos();
            int idUsuario = 0;
            try
            {
                datos.setearConsulta(@"SELECT IdUsuario FROM EMPLEADOS WHERE Id = @IdEmpleado");
                datos.setearParametro("@IdEmpleado", idEmpleado);
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
                datos.cerrarConexion();

                datos.setearConsulta(@"UPDATE EMPLEADOS SET 
                                        Legajo = @Legajo, 
                                        Cargo = @Cargo, 
                                        FechaIngreso = @FechaIngreso,
                                        FechaDespido = @FechaDespido 
                                     WHERE Id = @IdEmpleado");
                datos.setearParametro("@Legajo", legajo);
                datos.setearParametro("@Cargo", cargo);
                datos.setearParametro("@FechaIngreso", fechaIngreso);
                if (fechaDespido != null)
                    datos.setearParametro("@FechaDespido", fechaDespido);
                else
                    datos.setearParametro("@FechaDespido", DBNull.Value);
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
        public void agregar(int legajo, string cargo, DateTime fechaIngreso, DateTime? fechaDespido,
                              int documento, string nombre, string apellido, DateTime fechaNacimiento, int telefono,
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

                datos.setearConsulta(@"INSERT INTO EMPLEADOS (IdUsuario, Legajo, Cargo, FechaIngreso, FechaDespido) 
                                       VALUES (@IdInsertado, @Legajo, @Cargo, @FechaIngreso, @FechaDespido);");
                datos.setearParametro("@IdInsertado", idInsertado);
                datos.setearParametro("@Legajo", legajo);
                datos.setearParametro("@Cargo", cargo);
                datos.setearParametro("@FechaIngreso", fechaIngreso);
                if (fechaDespido != null)
                    datos.setearParametro("@FechaDespido", fechaDespido);
                else
                    datos.setearParametro("@FechaDespido", DBNull.Value);
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
        public void alternarEstado(int idEmpleado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE EMPLEADOS SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = @IdEmpleado");
                datos.setearParametro("@IdEmpleado", idEmpleado);
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
