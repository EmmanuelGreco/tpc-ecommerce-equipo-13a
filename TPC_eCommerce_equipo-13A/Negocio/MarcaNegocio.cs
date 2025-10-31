using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> listaMarca = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre FROM MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    listaMarca.Add(aux);
                }
                return listaMarca;
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

        public void agregar(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO MARCAS (Nombre) 
                                       VALUES (@nombre);");
                datos.setearParametro("@nombre", nombre);
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

        public void modificar(int id, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE MARCAS SET Nombre = @nombre
                                       WHERE Id = @id;");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@id", id);
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

        public bool eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id FROM ARTICULOS WHERE IdMarca = @idMarca");
                datos.setearParametro("@idMarca", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return false;
                }
                else
                {
                    datos.cerrarConexion();
                    datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id");
                    datos.setearParametro("@id", id);
                    datos.ejecutarAccion();
                    return true;
                } 
                
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

        public bool existe(string nombre, int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT Id FROM MARCAS WHERE Nombre = @nombre
                                       COLLATE SQL_Latin1_General_CP1_CS_AS AND Id <> @Id"); 
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                return datos.Lector.Read();
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
