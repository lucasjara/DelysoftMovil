using Delysoft.Apps.Usuario;
using ImageCircle.Forms.Plugin.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps
{
    public class Login : ContentPage
    {
        public Login()
        {
            // Declaración de Elementos
            var image = new Image
            {
                HeightRequest = 300,
                WidthRequest = 300,
                Margin = new Thickness(10, 20, 10, 10),
                HorizontalOptions = LayoutOptions.Center,
                Source = "logo_delysoft.png"
            };
            var margen = new Thickness(40, 0, 40, 0);
            Entry ent_username = new Entry { Placeholder = "Ingrese su Usuario ", Margin = margen };
            Entry ent_userpass = new Entry { IsPassword = true, Placeholder = "Ingrese su contraseña", Margin = margen };
            Button cmdIniciarSesion = new Button { Text = "Iniciar Sesión", Margin = margen };
            // Eventos Manejo de Login
            ent_username.Completed += (object sender, EventArgs e) => { ent_userpass.Focus(); };
            ent_userpass.Completed += (object sender, EventArgs e) => { cmdIniciarSesion.Focus(); };
            cmdIniciarSesion.Clicked += async (sender, e) =>
            {
                MetodosApi api = new MetodosApi();

                string username = ent_username.Text;
                string passuser = ent_userpass.Text;

                var respuesta = JArray.Parse(api.ValidarAcceso(username, passuser));
                if (respuesta[0].ToString() == "S")
                {
                    // Obtenemos el ID
                    Application.Current.Properties["id"]= respuesta[2].ToString();
                    var origen = "Login";

                    await Navigation.PushModalAsync(new PaginaMaestraUsuario(origen, null,null));
                }
                else
                {
                    await DisplayAlert("LOGIN", respuesta[1].ToString(), "OK");
                }
                
            };
            ent_userpass.Completed += (object sender, EventArgs e) =>
            {
                cmdIniciarSesion.Focus();
            };
            var contentView = new ContentView
            {
                Content = new StackLayout
                {
                    Children = {
                        image,
                         ent_username,
                         ent_userpass,
                         cmdIniciarSesion
                    }
                },
                ControlTemplate = formato_template
            };
            Content = contentView;
        }
        class FormatoTemplate : Grid
        {
            public FormatoTemplate()
            {
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.05, GridUnitType.Star) });
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.50, GridUnitType.Star) });
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.05, GridUnitType.Star) });
                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) });
                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.95, GridUnitType.Star) });

                var topBoxView = new BoxView { Color = Color.Orange };
                Children.Add(topBoxView, 0, 0);
                Grid.SetColumnSpan(topBoxView, 2);
                var topLabel = new Label
                {
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Italic
                };
                topLabel.SetBinding(Label.TextProperty, new TemplateBinding("Parent.HeaderText"));
                Children.Add(topLabel, 1, 0);

                var contentPresenter = new ContentPresenter();
                Children.Add(contentPresenter, 0, 1);
                Grid.SetColumnSpan(contentPresenter, 2);


                var bottomLabel = new Label
                {
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center
                };
                bottomLabel.SetBinding(Label.TextProperty, new TemplateBinding("Parent.FooterText"));
                Children.Add(bottomLabel, 1, 2);
            }
        }
        ControlTemplate formato_template = new ControlTemplate(typeof(FormatoTemplate));
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(Login), "Bienvenido a Delysoft");

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
    }
}