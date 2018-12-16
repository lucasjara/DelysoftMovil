using Delysoft.Apps.Repartidor.Pedido;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor
{

    public class MasterPageRepartidor : ContentPage
    {
        public ListView ListView { get { return listView; } }
        ListView listView;

        public MasterPageRepartidor()
        {
            var masterPageItems = new List<MasterPageItemRepartidor>();

            masterPageItems.Add(new MasterPageItemRepartidor
            {
                Title = "Inicio",
                IconSource = "menu_100x100.png",
                TargetType = typeof(RecargaRepartidor)
            });

            masterPageItems.Add(new MasterPageItemRepartidor
            {
                Title = "Historial de Pedidos",
                IconSource = "notificaciones2_100x100.png",
                TargetType = typeof(HistorialPedidosRepartidor)
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imageCell = new ImageCell();
                    imageCell.SetBinding(TextCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
                    return imageCell;
                }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None
            };

            Padding = new Thickness(0, 0, 0, 0);
            Icon = "menu_opciones_24x24.png";
            Title = "Opciones Repartidor";
            Label lbl_nombre = new Label
            {
                Text = Title,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5)
            };
            //Botón cerrar sesión
            Button btn_cerrar_sesion = new Button();
            btn_cerrar_sesion.Clicked += Btn_Cerrar_Sesion_Clicked;
            btn_cerrar_sesion.Text = "Cerrar sesión";
            btn_cerrar_sesion.BackgroundColor = Color.Red;
            btn_cerrar_sesion.TextColor = Color.White;

            Content = new StackLayout
            {
                Children =
                {
                    lbl_nombre,
                    listView,
                    btn_cerrar_sesion
                }
            };
        }
        async void Btn_Cerrar_Sesion_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Cerrar sesión", "Se ha cerrado la sesión", "OK");
            await this.Navigation.PushModalAsync(new Login());
        }
    }

}
