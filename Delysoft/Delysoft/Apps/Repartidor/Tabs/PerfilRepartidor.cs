using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor.Tabs
{
    public class PerfilRepartidor : ContentPage
    {
        public PerfilRepartidor()
        {
            var stack_uno = new StackLayout();
            var image = new CircleImage
            {
                BorderColor = Color.White,
                BorderThickness = 1,
                HeightRequest = 150,
                WidthRequest = 150,
                Source = "perfil_repartidor.png",
                Margin = new Thickness(5, 10, 5, 0)
            };
            Label lbl_nombre = new Label
            {
                Text = "Lmanns",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 30,
            };
            stack_uno.Children.Add(image);
            stack_uno.Children.Add(lbl_nombre);
            Label lbl_username = new Label
            {
                Text = "Nombre:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
            };
            Label lbl_username_det = new Label
            {
                Text = "Leonardo Manns",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20,
            };
            Label lbl_correo = new Label
            {
                Text = "Correo:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
            };
            Label lbl_correo_det = new Label
            {
                Text = "leonardomanns@outlook.cl",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20,
            };
            // Stack General
            var stack_general = new StackLayout();
            stack_general.Children.Add(stack_uno);
            stack_general.Children.Add(lbl_username);
            stack_general.Children.Add(lbl_username_det);
            stack_general.Children.Add(lbl_correo);
            stack_general.Children.Add(lbl_correo_det);
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