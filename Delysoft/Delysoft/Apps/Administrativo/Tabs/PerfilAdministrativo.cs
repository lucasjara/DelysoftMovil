using Delysoft.Apps.Usuario.Tabs.Model;
using ImageCircle.Forms.Plugin.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Administrativo.Tabs
{
    public class PerfilAdministrativo : ContentPage
    {
        public PerfilAdministrativo()
        {
            var stack_uno = new StackLayout() { BackgroundColor = Color.FromHex("#C0D5EB") };
            var image = new CircleImage
            {
                BorderColor = Color.Black,
                BorderThickness = 2,
                HeightRequest = 100,
                WidthRequest = 100,
                Source = "img_defecto_local.png",
                Margin = new Thickness(5, 10, 5, 5)
            };
            stack_uno.Children.Add(image);
            string id = Application.Current.Properties["id"] as string;
            MetodosApi api = new MetodosApi();
            // Stack General
            var stack_general = new StackLayout { BackgroundColor = Color.FromHex("#FFDAB1") };
            stack_general.Children.Add(stack_uno);

            var respuesta = JArray.Parse(api.ObtenerInformacionLocalAdministrativo(id));
            if (respuesta[0].ToString() == "S")
            {
                var jsonString = JArray.Parse(respuesta[1].ToString());
                var local = new LocalViewModel();

                foreach (JObject item in jsonString)
                {
                    local.Nombre = item.GetValue("LOCAL").ToString();
                    local.Region = item.GetValue("REGION").ToString();
                    local.Ciudad = item.GetValue("CIUDAD").ToString();
                }
                Label lbl_nombre_local = new Label
                {
                    Text = local.Nombre,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 32,
                };
                Label lbl_region = new Label
                {
                    Text = "Región:",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 15,
                };
                Label lbl_region_det = new Label
                {
                    Text = local.Region,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20,
                };
                Label lbl_ciudad = new Label
                {
                    Text = "Ciudad:",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 15,
                };
                Label lbl_ciudad_det = new Label
                {
                    Text = local.Ciudad,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20,
                };
                stack_general.Children.Add(lbl_nombre_local);
                stack_general.Children.Add(lbl_region);
                stack_general.Children.Add(lbl_region_det);
                stack_general.Children.Add(lbl_ciudad);
                stack_general.Children.Add(lbl_ciudad_det);
            }
            else
            {
                Label lbl_descripcion = new Label
                {
                    Text = "Error al Enviar",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20,
                };
                stack_general.Children.Add(lbl_descripcion);
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
    }
}