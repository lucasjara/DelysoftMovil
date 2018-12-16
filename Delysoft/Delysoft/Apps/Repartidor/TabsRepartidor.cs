using Delysoft.Apps.Repartidor.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor
{
    public class TabsRepartidor : TabbedPage
    {
        public TabsRepartidor()
        {
            var c = Color.FromHex("#3C454F");
            this.BarBackgroundColor = c;
            Children.Add(new PerfilRepartidor() { Title = "Perfil" });
            Children.Add(new ListadoPedidosRepartidor() { Title = "Pedidos" });
            Children.Add(new MapaSeguimiento() { Title = "En Curso" });
        }
    }
}