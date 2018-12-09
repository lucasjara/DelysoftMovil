using Delysoft.Apps.Usuario.Pedido;
using Delysoft.Apps.Usuario.Pedido.Model;
using Delysoft.Apps.Usuario.Tabs;
using Delysoft.Apps.Usuario.Tabs.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Usuario
{
    public class PaginaMaestraUsuario : MasterDetailPage
    {
        MasterPage masterPage;
        /*
         * ======== Pagina Maestra del Usuario
         * ==============================================
         *  origen = "Pantalla a la que desea Acceder".
         *  objetos = "Arreglo con los distintos objetos con la informacion a desplegar en cada pantalla."
         *  objetos[0] = ProductoViewModel
         *  objetos[1] = PedidoViewModel
         */
        public PaginaMaestraUsuario(String origen, ProductoViewModel prod, PedidoViewModel ped)
        {
            masterPage = new MasterPage();
            Master = masterPage;
            switch (origen) {
                case "Login":case "Cambio":
                    Detail = new NavigationPage(new TabsUser());
                    break;
                case "VistaPrevia":
                    Detail = new NavigationPage(new VistaPrevia(prod));
                    break;
                case "DetallePedido":
                    Detail = new NavigationPage(new DetallePedido(ped));
                    break;
                default:
                    break;
            }

            /*
            else if (origen == "1")
            {
                Detail = new NavigationPage(new VistaPrevia(foo));
            }
            else if (origen == "2")
            {
                
            }
            else if (origen == "3")
            {
                Detail = new NavigationPage(new TabsRepartidor());
            }
            else if (origen == "4")
            {
                Detail = new NavigationPage(new DetallePedidoRepartidor(ped));
            }
            else if (origen == "5")
            {
                Detail = new NavigationPage(new PPrincipal());
            }
            */
            masterPage.ListView.ItemSelected += OnItemSelected;
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}