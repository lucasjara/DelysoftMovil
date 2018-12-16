using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Administrativo
{
    public class RecargaAdministrativo : ContentPage
    {
        public RecargaAdministrativo()
        {
            string origen = "Cambio";
            CambioPantalla(origen);
        }

        private async System.Threading.Tasks.Task CambioPantalla(string origen)
        {
            await Navigation.PushModalAsync(new PaginaMaestraAdministrativo(origen, null, null));
        }
    }
}