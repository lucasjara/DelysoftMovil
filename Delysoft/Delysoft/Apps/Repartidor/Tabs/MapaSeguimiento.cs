using Delysoft.Apps.Usuario.Pedido.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Delysoft.Apps.Repartidor.Tabs
{
    public class MapaSeguimiento : ContentPage
    {
        public ObservableCollection<PedidoViewModel> pedido { get; set; }
        public MapaSeguimiento()
        {
            var stack = new StackLayout { Spacing = 0 };
            Button cmdObtener = new Button
            {
                Text = "Ver Ubicacion Pedido"
            };
            var map = new Xamarin.Forms.Maps.Map();
            cmdObtener.Clicked += async (sender, e) =>
            {
                MetodosApi api = new MetodosApi();
                //var respuesta = JArray.Parse(api.ObtenerUbicacionPedido("6"));
                /*if (respuesta[0].ToString() == "S")
                {
                    JArray jsonString = JArray.Parse(respuesta[1].ToString());
                    string longi = "";
                    string lati = "";
                    foreach (JObject item in jsonString)<
                    {
                        longi = item.GetValue("LONGITUD").ToString();
                        lati = item.GetValue("LATITUD").ToString();
                        pedido.Add(new PedidoViewModel
                        {
                            IdPedido = item.GetValue("ID_PRODUCTO").ToString(),
                            Longitud = item.GetValue("LONGITUD").ToString(),
                            Latitud = item.GetValue("LATITUD").ToString(),
                        });
                    }
                    Double lat_odu = Convert.ToDouble(lati.Replace('.', ','));
                    Double longi_dou = Convert.ToDouble(longi.Replace('.', ','));
                    */
                    stack.Children.Remove(map);
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    var latitud = location.Latitude;
                    var longitud = location.Longitude;
                    var altitud = location.Altitude;

                    map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(new Position(latitud, longitud), Distance.FromMiles(0.3)))
                    {
                        IsShowingUser = true,
                        HeightRequest = 100,
                        WidthRequest = 960,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
                    var pin = new Pin
                    {
                        Position = new Position(-38.725406,-72.632708 ),
                        Label = "Lugar de Llegada"
                    };
                    map.Pins.Add(pin);
                    stack.Children.Add(map);
                /*}
                else
                {
                    await DisplayAlert("Alerta",respuesta["1"].ToString(),"OK");
                }
                */
            };
            stack.Children.Add(cmdObtener);
            Content = stack;
        }
    }
}