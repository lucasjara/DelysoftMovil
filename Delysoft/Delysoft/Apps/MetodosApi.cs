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
        /*
         * Metodo para Obtener los Productos disponibles segun la ubicacion
         */
        public string ObtenerListadoProductosDisponibles(string id, double latitud, double longitud)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("https://www.infest.cl/servicios/api/usuarios/obtener_oferta_productos");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "id", id },
                        { "latitud",latitud.ToString()},
                        { "longitud",longitud.ToString()}
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
        /*
         * Metodo para enviar Datos del pedido
         */
        public string EnviarDatosPedido(string id_usuario, string cantidad, string id_producto, string observacion, double latitud, double longitud)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("http://www.infest.cl/servicios/api/usuarios/crear_pedido_local/");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "id_usuario", id_usuario },
                        { "cantidad", cantidad },
                        { "id_prod", id_producto },
                        { "observacion", "Avenida Siempreviva 742" },
                        { "latitud",latitud.ToString()},
                        { "longitud",longitud.ToString()}
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
        /*
         *  Metodo para obtener el historial de pedido del usuario
         */
        public string ObtenerHistorialPedidos(string id)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("https://www.infest.cl/servicios/api/usuarios/obtener_listado_historico_usuario");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "id", id }
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
        /*
         * Listado de Locales Favoritos del Usuario
         */ 
        public string ObtenerListadoLocalesFavoritos(string id)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("https://www.infest.cl/servicios/api/usuarios/obtener_listado_locales_favoritos");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "id", id },
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
