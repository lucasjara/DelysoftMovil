using Delysoft.Apps.Usuario.Pedido.Model;
using Delysoft.Apps.Usuario.Tabs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor
{
    public class PaginaMaestraRepartidor : MasterDetailPage
    {
        MasterPageRepartidor masterPage;
        public PaginaMaestraRepartidor(String origen, ProductoViewModel prod, PedidoViewModel ped)
        {
            masterPage = new MasterPageRepartidor();
            Master = masterPage;

            switch (origen)
            {
                case "Login":
                case "Cambio":
                    Detail = new NavigationPage(new TabsRepartidor());
                    break;
                case "VistaPrevia":
                    // Detail = new NavigationPage(new VistaPrevia(prod));
                    break;
                case "DetallePedido":
                    // Detail = new NavigationPage(new DetallePedido(ped));
                    break;
                default:
                    break;
            }
            masterPage.ListView.ItemSelected += OnItemSelected;
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItemRepartidor;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}