using Delysoft.Apps.Repartidor.Pedido;
using Delysoft.Apps.Usuario.Pedido.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor.Tabs
{

    public class ListadoPedidosRepartidor : ContentPage
    {
        public ObservableCollection<PedidoViewModel> pedido { get; set; }

        public ListadoPedidosRepartidor()
        {
            pedido = new ObservableCollection<PedidoViewModel>();
            ListView lstView = new ListView();
            // ID que debemos obtener de la app
            string id = "6";
            var respuesta = JArray.Parse(ObtenerHistorialPedidos(id));
            // var respuesta = JArray.Parse("[{'ID_'}]");
            if (respuesta[0].ToString() == "S")
            {
                lstView.RowHeight = 60;
                lstView.ItemTemplate = new DataTemplate(typeof(FormatoCelda));
                JArray jsonString = JArray.Parse(respuesta[1].ToString());

                foreach (JObject item in jsonString)
                {
                    pedido.Add(new PedidoViewModel
                    {
                        IdPedido = item.GetValue("ID_ENC").ToString(),
                        NombreProducto = item.GetValue("PRODUCTO").ToString(),
                        Local = item.GetValue("LOCAL").ToString(),
                        Precio = item.GetValue("PRECIO").ToString(),
                        EstadoPedido = item.GetValue("ESTADO_PEDIDO").ToString(),
                        Cantidad = item.GetValue("CANTIDAD").ToString(),
                        Total = item.GetValue("TOTAL").ToString(),
                        TipoPago = item.GetValue("TIPO_PAGO").ToString(),
                        Imagen = "mapa.jpg",
                        Observacion = item.GetValue("OBSERVACION").ToString(),
                        Fecha = item.GetValue("FECHA").ToString()
                    });
                }
                lstView.ItemsSource = pedido;
                lstView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
                {
                    var content = e.Item as PedidoViewModel;

                    await Navigation.PushModalAsync(new DetallePedidoRepartidor(content));
                };
            }
            else
            {
                lstView.RowHeight = 25;
                lstView.ItemTemplate = new DataTemplate(typeof(SinFormato));
                pedido.Add(new PedidoViewModel { NombreProducto = respuesta[1].ToString() });
                lstView.ItemsSource = pedido;
            }

            Content = lstView;
        }
        public class FormatoCelda : ViewCell
        {
            public FormatoCelda()
            {
                //instantiate each of our views
                var imagen = new Image();
                var titulo = new Label();
                var estado_pedido = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { };

                titulo.SetBinding(Label.TextProperty, new Binding("NombreProducto"));
                estado_pedido.SetBinding(Label.TextProperty, new Binding("EstadoPedido"));
                imagen.SetBinding(Image.SourceProperty, new Binding("Imagen"));

                imagen.HorizontalOptions = LayoutOptions.Start;
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                titulo.FontSize = 20;
                estado_pedido.FontSize = 20;

                verticaLayout.Children.Add(titulo);
                verticaLayout.Children.Add(estado_pedido);
                horizontalLayout.Children.Add(imagen);
                horizontalLayout.Children.Add(verticaLayout);

                View = horizontalLayout;
            }
        }
        public class SinFormato : ViewCell
        {
            public SinFormato()
            {
                //instantiate each of our views
                var nombre = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { };

                nombre.SetBinding(Label.TextProperty, new Binding("NombreProducto"));
                nombre.FontSize = 20;
                nombre.HorizontalOptions = LayoutOptions.Center;
                verticaLayout.Children.Add(nombre);
                horizontalLayout.Children.Add(verticaLayout);
                View = horizontalLayout;
            }
        }
        private string ObtenerHistorialPedidos(string id)
        {
            string respuestaString = "";
            try
            {
                WebClient cliente = new WebClient();
                Uri uri = new Uri("https://www.infest.cl/servicios/api/usuarios/obtener_pedidos_pendientes_repartidor");
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

    }
}