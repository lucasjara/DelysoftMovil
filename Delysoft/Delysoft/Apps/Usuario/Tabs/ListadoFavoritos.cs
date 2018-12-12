using Delysoft.Apps.Usuario.Tabs.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Usuario.Tabs
{
    public class ListadoFavoritos : ContentPage
    {
        public ObservableCollection<LocalViewModel> local { get; set; }
        public ListadoFavoritos()
        {
            local = new ObservableCollection<LocalViewModel>();
            ListView lstView = new ListView();
            // ID que debemos obtener de la app
            string id = Application.Current.Properties["id"] as string;
            MetodosApi api = new MetodosApi();
            var respuesta = JArray.Parse(api.ObtenerListadoLocalesFavoritos(id));
            if (respuesta[0].ToString() == "S")
            {
                lstView.RowHeight = 60;
                lstView.ItemTemplate = new DataTemplate(typeof(FormatoCelda));
                JArray jsonString = JArray.Parse(respuesta[1].ToString());
                foreach (JObject item in jsonString)
                {
                    local.Add(new LocalViewModel { Nombre = item.GetValue("LOCAL").ToString(), Descripcion = item.GetValue("DESCRIPCION").ToString(), Imagen = "img_defecto_local.png" });
                }
            }
            else
            {
                lstView.RowHeight = 20;
                lstView.ItemTemplate = new DataTemplate(typeof(SinFormato));
                local.Add(new LocalViewModel { Nombre = respuesta[1].ToString() });
            }

            lstView.ItemsSource = local;
            Content = lstView;
        }

        public class FormatoCelda : ViewCell
        {
            public FormatoCelda()
            {
                //instantiate each of our views
                var imagen = new Image();
                var nombre = new Label();
                var descripcion = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { };

                nombre.SetBinding(Label.TextProperty, new Binding("Nombre"));
                descripcion.SetBinding(Label.TextProperty, new Binding("Descripcion"));
                imagen.SetBinding(Image.SourceProperty, new Binding("Imagen"));

                imagen.HorizontalOptions = LayoutOptions.Start;
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                nombre.FontSize = 20;
                descripcion.FontSize = 15;

                verticaLayout.Children.Add(nombre);
                verticaLayout.Children.Add(descripcion);
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

                nombre.SetBinding(Label.TextProperty, new Binding("Nombre"));
                nombre.FontSize = 20;
                nombre.HorizontalOptions = LayoutOptions.Center;
                verticaLayout.Children.Add(nombre);
                horizontalLayout.Children.Add(verticaLayout);
                View = horizontalLayout;
            }
        }
    }

}