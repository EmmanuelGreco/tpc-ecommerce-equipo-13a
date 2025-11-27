using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FuncionesAdicionales
    {
        public string generarContrasenia(string nombre, string documento)
        {

            string contrasenia = documento + nombre.Replace(" ", "");
            if (contrasenia.Length > 16)
                contrasenia = contrasenia.Substring(0, 16);
            contrasenia = $"Pa{contrasenia}1!";
            return contrasenia;
        }
    }
}
