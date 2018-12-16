using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Repartidor
{
    public class RecargaRepartidor : ContentPage
    {
        public RecargaRepartidor()
        {
            string origen = "Cambio";
            CambioPantalla(origen);
        }

        private async System.Threading.Tasks.Task CambioPantalla(string origen)
        {
            await Navigation.PushModalAsync(new PaginaMaestraRepartidor(origen, null, null));
        }
    }
}