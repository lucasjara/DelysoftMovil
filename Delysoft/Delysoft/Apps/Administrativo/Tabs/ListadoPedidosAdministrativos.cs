using Delysoft.Apps.Administrativo.Pedido;
using Delysoft.Apps.Usuario.Pedido.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Administrativo.Tabs
{
    public class ListadoPedidosAdministrativos : ContentPage
    {
        public ObservableCollection<PedidoViewModel> pedido { get; set; }
        public ListadoPedidosAdministrativos()
        {
            pedido = new ObservableCollection<PedidoViewModel>();
            ListView lstView = new ListView();
            // ID que debemos obtener de la app
            string id = Application.Current.Properties["id"] as string;
            MetodosApi api = new MetodosApi();
            var respuesta = JArray.Parse(api.ObtenerPedidosAdministrativo(id));
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
                        Fecha = item.GetValue("FECHA").ToString(),
                        Longitud = item.GetValue("LONGITUD").ToString(),
                        Latitud = item.GetValue("LATITUD").ToString()
                    });
                }
            }
            else
            {
                lstView.RowHeight = 25;
                lstView.ItemTemplate = new DataTemplate(typeof(SinFormato));
                pedido.Add(new PedidoViewModel { NombreProducto = respuesta[1].ToString() });
            }
            lstView.ItemsSource = pedido;
            lstView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                var content = e.Item as PedidoViewModel;
                await Navigation.PushModalAsync(new DetallePedidoAdministrativo(content));
            };
            Content = lstView;
        }
        public class FormatoCelda : ViewCell
        {
            public FormatoCelda()
            {
                //instantiate each of our views
                var imagen = new Image();
                var titulo = new Label();
                var estado = new Label();
                var total = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { };

                titulo.SetBinding(Label.TextProperty, new Binding("NombreProducto"));
                estado.SetBinding(Label.TextProperty, new Binding("EstadoPedido"));
                total.SetBinding(Label.TextProperty, new Binding("Total", stringFormat: "$ {0}"));
                imagen.SetBinding(Image.SourceProperty, new Binding("Imagen"));

                imagen.HorizontalOptions = LayoutOptions.Start;
                total.VerticalOptions = LayoutOptions.Center;
                total.HorizontalOptions = LayoutOptions.CenterAndExpand;
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                titulo.FontSize = 20;
                estado.FontSize = 20;
                total.FontSize = 20;

                verticaLayout.Children.Add(titulo);
                verticaLayout.Children.Add(estado);
                horizontalLayout.Children.Add(imagen);
                horizontalLayout.Children.Add(verticaLayout);
                horizontalLayout.Children.Add(total);

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
    }
}