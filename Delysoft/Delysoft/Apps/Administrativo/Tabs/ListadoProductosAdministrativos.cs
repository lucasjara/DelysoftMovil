using Delysoft.Apps.Administrativo.Pedido;
using Delysoft.Apps.Usuario.Tabs.Model;
using ImageCircle.Forms.Plugin.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Administrativo.Tabs
{
    public class ListadoProductosAdministrativos : ContentPage
    {
        public ObservableCollection<ProductoViewModel> producto { get; set; }
        public ListadoProductosAdministrativos()
        {
            producto = new ObservableCollection<ProductoViewModel>();
            ListView lstView = new ListView();
            // ID que debemos obtener de la app
            string id = Application.Current.Properties["id"] as string;
            MetodosApi api = new MetodosApi();
            var respuesta = JArray.Parse(api.ObtenerListadoProductosLocal(id));
            // var respuesta = JArray.Parse("[{'ID_'}]");
            if (respuesta[0].ToString() == "S")
            {
                lstView.RowHeight = 60;
                lstView.ItemTemplate = new DataTemplate(typeof(FormatoCelda));
                JArray jsonString = JArray.Parse(respuesta[1].ToString());
                foreach (JObject item in jsonString)
                {
                    string estado_pedido = item.GetValue("ESTADO_PEDIDO").ToString();
                    if (estado_pedido == "S")
                    {
                        estado_pedido = "Activo";
                    }
                    else
                    {
                        estado_pedido = "Inactivo";
                    }
                    producto.Add(new ProductoViewModel
                    {
                        Id = item.GetValue("ID_PRODUCTO").ToString(),
                        Nombre = item.GetValue("PRODUCTO").ToString(),
                        Descripcion = item.GetValue("DESCRIPCION").ToString(),
                        Precio = dar_formato(item.GetValue("PRECIO").ToString()),
                        Imagen = "sushix100.jpg",
                        Local = item.GetValue("LOCAL").ToString(),
                        ImagenProducto = "sin_foto.png",
                        ZonaProducto = item.GetValue("ZONA_PRODUCTOS").ToString(),
                        EstadoProducto = estado_pedido
                    });
                }
            }
            else
            {
                lstView.RowHeight = 25;
                lstView.ItemTemplate = new DataTemplate(typeof(SinFormato));
                producto.Add(new ProductoViewModel { Nombre = respuesta[1].ToString() });
            }
            lstView.ItemsSource = producto;
            lstView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                var content = e.Item as ProductoViewModel;
                var respuesta_cambio = JArray.Parse(api.CambiarEstadoProductosLocal(content.Id, content.EstadoProducto));
                if (respuesta_cambio[0].ToString() == "S")
                {
                    await Navigation.PushModalAsync(new PaginaMaestraAdministrativo("Cambio", null, null));
                }
                else
                {
                    await DisplayAlert("Alerta", respuesta_cambio[1].ToString(), "OK");
                }

            };
            Content = lstView;
        }
        public class FormatoCelda : ViewCell
        {
            public FormatoCelda()
            {
                // Instancias de Imagenes
                var imagenproducto = new Image
                {
                    WidthRequest = 300,
                    Aspect = Aspect.AspectFill
                };
                var imagen = new CircleImage
                {
                    BorderColor = Color.Black,
                    BorderThickness = 2,
                    HeightRequest = 60,
                    WidthRequest = 60,
                    Aspect = Aspect.AspectFit,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = 2
                };
                // Instancias de Textos
                var nombre = new Label();
                var descripcion = new Label();
                var precio = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.End, Margin = new Thickness(0, 0, 10, 0) };
                var zona_producto = new Label();
                var estado_pedido = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, Margin = new Thickness(0, 0, 10, 0) };

                var verticaLayout = new StackLayout();
                var verticaLayout_dos = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
                var horizontalLayout = new StackLayout();

                nombre.SetBinding(Label.TextProperty, new Binding("Nombre"));
                zona_producto.SetBinding(Label.TextProperty, new Binding("ZonaProducto"));
                precio.SetBinding(Label.TextProperty, new Binding("Precio"));
                estado_pedido.SetBinding(Label.TextProperty, new Binding("EstadoProducto"));

                imagen.SetBinding(Image.SourceProperty, new Binding("Imagen"));

                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

                nombre.FontSize = 20;
                zona_producto.FontSize = 20;
                precio.FontSize = 20;
                estado_pedido.FontSize = 20;

                verticaLayout.Children.Add(nombre);
                verticaLayout.Children.Add(zona_producto);

                horizontalLayout.Children.Add(imagen);
                horizontalLayout.Children.Add(verticaLayout);
                verticaLayout_dos.Children.Add(precio);
                verticaLayout_dos.Children.Add(estado_pedido);
                horizontalLayout.Children.Add(verticaLayout_dos);
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
                nombre.SetBinding(Label.TextProperty, new Binding("Nombre"));
                nombre.FontSize = 20;
                nombre.HorizontalOptions = LayoutOptions.Center;
                verticaLayout.Children.Add(nombre);
                horizontalLayout.Children.Add(verticaLayout);
                View = horizontalLayout;
            }
        }
        private string dar_formato(string numero)
        {
            int numVal = Int32.Parse(numero);
            return numVal.ToString("C");
        }
    }
}