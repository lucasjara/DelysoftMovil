using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Delysoft.Apps
{
    public class MetodosApi
    {
        /*
         * Metodo para Validar el Acceso al Sistema.
         */
        public string ValidarAcceso(string username, string passuser)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("http://www.infest.cl/servicios/api/usuarios/validar_usuario/");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "usuario", username },
                        { "password", passuser }
                    };
                byte[] respuestaByte = cliente.UploadValues(uri, "POST", parametros);
                respuestaString = Encoding.UTF8.GetString(respuestaByte);
            }
            catch (Exception)
            {
                respuestaString = "[\"N\",\"Error al Enviar la petición.\"]";
            }
            return respuestaString;
        }
    }
}
