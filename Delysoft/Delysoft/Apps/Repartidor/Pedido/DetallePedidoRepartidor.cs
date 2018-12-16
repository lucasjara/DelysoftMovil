using Delysoft.Apps.Usuario.Pedido;
using Delysoft.Apps.Usuario.Pedido.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor.Pedido
{
    public class DetallePedidoRepartidor : ContentPage
    {
        public string valor = "";
        public string id_pedido = "";
        public DetallePedidoRepartidor(PedidoViewModel ped)
        {
            id_pedido = ped.IdPedido;
            // Elementos Titulo y Imagen
            var stack_uno = new StackLayout { VerticalOptions = LayoutOptions.Center };

            Label lbl_delyvery = new Label
            {
                Text = ped.Local + " - " + ped.NombreProducto,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 13,
                Margin = new Thickness(5)
            };
            Image imagen = new Image { Source = "sushi.jpg", Margin = new Thickness(10, 0, 10, 5) };
            Label lbl_observacion = new Label
            {
                Text = "Direccion: " + ped.Observacion,
                FontSize = 15,
                Margin = new Thickness(5, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center
            };
            stack_uno.Children.Add(lbl_delyvery);
            stack_uno.Children.Add(imagen);
            stack_uno.Children.Add(lbl_observacion);
            // Elementos Detalle Cantidad - Precio - Total
            var stack_dos = new StackLayout { VerticalOptions = LayoutOptions.End };

            Label lbl_Cantidad = new Label { Text = "Cantidad:", FontSize = 10 };
            Label lbl_Precio = new Label { Text = "Precio Unitario:", FontSize = 10 };
            Label lbl_Total = new Label { Text = "Total:", FontSize = 10 };
            Label lbl_Tipo = new Label { Text = "Tipo de Pago:", FontSize = 10 };
            Label lbl_Estado = new Label { Text = "Estado:", FontSize = 10 };

            Label lbl_cantidad_total = new Label { Text = ped.Cantidad, FontSize = 20 };
            Label lbl_valor_Precio = new Label { Text = formatoPeso(ped.Precio), FontSize = 20 };
            Label lbl_valor_Total = new Label { Text = formatoPeso(ped.Total), FontSize = 20 };
            Label lbl_tipo_txt = new Label { Text = ped.TipoPago, FontSize = 20 };
            string valor = "";
            if (ped.EstadoPedido.Substring(0, 3) == "Can")
            {
                valor = "Cancelado";
            }
            else
            {
                valor = ped.EstadoPedido;
            }
            Label lbl_estado_txt = new Label { Text = valor, FontSize = 20, Margin = new Thickness(0, 0, 0, 17) };

            stack_dos.Children.Add(lbl_Cantidad);
            stack_dos.Children.Add(lbl_cantidad_total);
            stack_dos.Children.Add(lbl_Precio);
            stack_dos.Children.Add(lbl_valor_Precio);
            stack_dos.Children.Add(lbl_Total);
            stack_dos.Children.Add(lbl_valor_Total);
            stack_dos.Children.Add(lbl_Tipo);
            stack_dos.Children.Add(lbl_tipo_txt);
            stack_dos.Children.Add(lbl_Estado);
            stack_dos.Children.Add(lbl_estado_txt);
            // Definición Grilla
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            // Agregar Stack uno y dos en Grilla
            grid.Children.Add(stack_uno, 0, 0);
            grid.Children.Add(stack_dos, 1, 0);
            // Stack General
            var stack_general = new StackLayout();
            var stack_general_total = new StackLayout { Margin = new Thickness(10, 20, 10, 0), BackgroundColor = Color.White };
            stack_general_total.Children.Add(grid);
            stack_general.Children.Add(stack_general_total);

            valor = ped.EstadoPedido;
            string texto = "";
            switch (valor)
            {
                case "Enviado":
                    texto = "En Camino";
                    break;
                case "En Camino":
                    texto = "En Destino";
                    break;
                case "En Destino":
                    texto = "Entregar";
                    break;
                default:
                    texto = "";
                    break;
            }
            Button btn_cambio_estado = new Button
            {
                Text = texto,
                Margin = new Thickness(20, 0, 20, 0)
            };
            if (texto != "")
            {
                btn_cambio_estado.Clicked += async (sender, e) =>
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    var latitude = location.Latitude;
                    var longitud = location.Longitude;
                    var resultado = JArray.Parse(EnviarDatosPedido(ped.EstadoPedido, ped.IdPedido, latitude, longitud));
                    if (resultado[0].ToString() == "S")
                    {
                        ped.EstadoPedido = resultado[1].ToString();
                        await Navigation.PushModalAsync(new PaginaMaestraRepartidor("Cambio",null,null));
                    }
                    else
                    {
                        await DisplayAlert("Alerta", resultado[1].ToString(), "OK");
                    }

                };
                stack_general.Children.Add(btn_cambio_estado);
            }
            // Contenido Vista
            var contentView = new ContentView
            {
                Content = stack_general,
                //ControlTemplate = pruebatemplate,
                BackgroundColor = Color.FromHex("#E9E9E9")
            };
            Content = contentView;
        }


        string EnviarDatosPedido(string estado_pedido, string id_pedido, double latitud, double longitud)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("http://www.infest.cl/servicios/api/usuarios/cambiar_estado_pedido_repartidor/");
                NameValueCollection parametros = new NameValueCollection
                    {
                        { "id_pedido", id_pedido },
                        { "estado_pedido", estado_pedido },
                        { "latitud",latitud.ToString()},
                        { "longitud",longitud.ToString()}
                    };
                byte[] respuestaByte = cliente.UploadValues(uri, "POST", parametros);
                respuestaString = Encoding.UTF8.GetString(respuestaByte);
            }
            catch (Exception ex)
            {
                respuestaString = "[\"N\",\"Error al Enviar la petición.\"]";
            }
            return respuestaString;
        }

        private string formatoPeso(string valor)
        {
            int numero = Int32.Parse(valor);
            return numero.ToString("C");
        }


    }
}