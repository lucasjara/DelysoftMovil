using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Delysoft.Apps
{
    public class PaginaCambioPerfil : ContentPage
    {
        public PaginaCambioPerfil()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Pagina para cambiarse de Perfil!" }
                }
            };
        }
    }
}