using Delysoft.Apps.Administrativo.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Administrativo
{
    public class TabsAdministrativo : TabbedPage
    {
        public TabsAdministrativo()
        {
            var c = Color.FromHex("#3C454F");
            this.BarBackgroundColor = c;
            Children.Add(new PerfilAdministrativo() { Title = "Perfil" });
            Children.Add(new ListadoPedidosAdministrativos() { Title = "Pedidos" });
            Children.Add(new ListadoProductosAdministrativos() { Title = "Productos" });
        }
    }
}