using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor.Pedido
{
    public class HistorialPedidosRepartidor : ContentPage
    {
        public HistorialPedidosRepartidor()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}