using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps.Usuario
{
    public class RecargaUsuario : ContentPage
    {
        public RecargaUsuario()
        {
            string origen = "Cambio";
            CambioPantalla(origen);
        }

        private async System.Threading.Tasks.Task CambioPantalla(string origen)
        {
            await Navigation.PushModalAsync(new PaginaMaestraUsuario(origen,null,null));
        }
    }
}