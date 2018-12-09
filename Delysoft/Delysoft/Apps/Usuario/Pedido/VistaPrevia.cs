using Delysoft.Apps.Usuario.Pedido.Model;
using Delysoft.Apps.Usuario.Tabs.Model;
using Newtonsoft.Json.Linq;
using System;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delysoft.Apps.Usuario.Pedido
{
    class VistaPrevia : ContentPage
    {
        // Declaracion Elementos Publicos
        Switch switcher = new Switch { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
        Switch switcher_dos = new Switch { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };

        Label lbl_valor_Total = new Label { FontSize = 20 };
        Label lbl_cantidad_total = new Label { Text = "1" };
        private object prod;

        public VistaPrevia(ProductoViewModel prod)
        {
            // Elementos Titulo y Imagen
            var stack_uno = new StackLayout { VerticalOptions = LayoutOptions.Center };

            Label lbl_delyvery = new Label { Text = prod.Local + " - " + prod.Nombre, HorizontalTextAlignment = TextAlignment.Center, FontSize = 13, Margin = new Thickness(5) };
            Image imagen = new Image { Source = "sushi.jpg", Margin = new Thickness(10, 0, 10, 10) };
            stack_uno.Children.Add(lbl_delyvery);
            stack_uno.Children.Add(imagen);
            // Elementos Detalle Cantidad - Precio - Total
            var stack_dos = new StackLayout { VerticalOptions = LayoutOptions.End };

            Label lbl_Cantidad = new Label { Text = "Cantidad:", FontSize = 10 };
            Label lbl_Precio = new Label { Text = "Precio Unitario:", FontSize = 10 };
            Label lbl_valor_Precio = new Label { Text = prod.Precio, FontSize = 20 };
            Label lbl_Total = new Label { Text = "Total:", FontSize = 10 };
            lbl_valor_Total.Text = prod.Precio;

            Button btn_mas = new Button { Image = "plus.png", CornerRadius = 50, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(18, 0, 18, 0) };
            Button btn_menos = new Button { Image = "minus.png", CornerRadius = 50, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(18, 0, 18, 0) };


            btn_mas.Clicked += (sender, e) =>
            {
                int numero = Int32.Parse(lbl_cantidad_total.Text) + 1;
                string precio_string = lbl_valor_Precio.Text;

                string resultado = precio_string.Replace("$", "");
                resultado = resultado.Replace(".", "");
                int precio = Int32.Parse(resultado);
                int total = numero * precio;
                // Remplazar Valor Textos
                lbl_cantidad_total.Text = numero.ToString();
                lbl_valor_Total.Text = total.ToString("C");
            };
            btn_menos.Clicked += (sender, e) =>
            {
                int numero = Int32.Parse(lbl_cantidad_total.Text) - 1;

                if (numero != 0)
                {
                    string precio_string = lbl_valor_Precio.Text;
                    string resultado = precio_string.Replace("$", "");
                    resultado = resultado.Replace(".", "");
                    int precio = Int32.Parse(resultado);
                    int total = numero * precio;
                    // Remplazar Valor Textos
                    lbl_cantidad_total.Text = numero.ToString();
                    lbl_valor_Total.Text = total.ToString("C");
                }
            };
            //----------------------------
            stack_dos.Children.Add(btn_mas);
            stack_dos.Children.Add(btn_menos);
            stack_dos.Children.Add(lbl_Cantidad);
            stack_dos.Children.Add(lbl_cantidad_total);
            stack_dos.Children.Add(lbl_Precio);
            stack_dos.Children.Add(lbl_valor_Precio);
            stack_dos.Children.Add(lbl_Total);
            stack_dos.Children.Add(lbl_valor_Total);
            // Elementos Tipo de Pago
            Label lbl_efectivo = new Label { Text = "Efectivo Justo", FontSize = 15, VerticalTextAlignment = TextAlignment.Center };
            Label lbl_sobre = new Label { Text = "SobreEfectivo", FontSize = 15, VerticalTextAlignment = TextAlignment.Center };
            Label lbl_efectivo_detalle = new Label { Text = "(Entregara el monto exacto que aparece en el total)", FontSize = 8, VerticalTextAlignment = TextAlignment.Center };
            Label lbl_sobre_detalle = new Label { Text = "(Entregara un monto superior al que aparece en el Total)", FontSize = 8, VerticalTextAlignment = TextAlignment.Center };
            Entry ent_monto = new Entry { Placeholder = "Ingrese el Monto con el que pagara", Keyboard = Keyboard.Numeric };
            Entry ent_observacion = new Entry { Placeholder = "Ingrese Direccion de Entrega", Keyboard = Keyboard.Text };

            var stack_tres = new StackLayout();
            var stack_tres_uno = new StackLayout { Orientation = StackOrientation.Horizontal };
            var stack_tres_dos = new StackLayout { Orientation = StackOrientation.Horizontal };

            stack_tres_uno.Children.Add(switcher);
            stack_tres_uno.Children.Add(lbl_efectivo);
            stack_tres_uno.Children.Add(lbl_efectivo_detalle);

            stack_tres_dos.Children.Add(switcher_dos);
            stack_tres_dos.Children.Add(lbl_sobre);
            stack_tres_dos.Children.Add(lbl_sobre_detalle);

            switcher.Toggled += switcher_Toggled;
            switcher_dos.Toggled += switcher_Toggled_dos;
            switcher.IsToggled = true;

            stack_tres.Children.Add(stack_tres_uno);
            stack_tres.Children.Add(stack_tres_dos);
            stack_tres.Children.Add(ent_monto);

            // Elementos Final Siguiente / Cancelar
            var stack_cuatro = new StackLayout { Margin = new Thickness(5) };
            Button cmdSiguiente = new Button { Text = "Siguiente", BackgroundColor = Color.FromHex("#FF8800"), TextColor = Color.FromHex("#FFFFFF"), Margin = new Thickness(30, 0, 30, 0) };
            Button cmdCancelar = new Button { Text = "Cancelar Pedido", BackgroundColor = Color.FromHex("#47525E"), TextColor = Color.FromHex("#FFFFFF"), Margin = new Thickness(30, 0, 30, 0) };
            // Eventos Siguiente / Cancelar
            cmdSiguiente.Clicked += async (sender, e) =>
            {
                Boolean flag = false;
                if (switcher.IsToggled) { flag = true; }
                else if (switcher_dos.IsToggled)
                {
                    if (ent_monto.Text != null && Int32.Parse(ent_monto.Text) >= 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Monto no Valido al Elegir SobreEfectivo", "OK");
                    }
                }
                if (flag)
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    var latitude = location.Latitude;
                    var longitud = location.Longitude;
                    MetodosApi api = new MetodosApi();
                    var respuesta = JArray.Parse(api.EnviarDatosPedido("1", lbl_cantidad_total.Text, prod.Id, "", latitude, longitud));
                    if (respuesta[0].ToString() == "S")
                    {
                        var jsonString = JArray.Parse(respuesta[2].ToString());
                        //await DisplayAlert("Alerta", jsonString, "OK");

                        var pedido = new PedidoViewModel();

                        foreach (JObject item in jsonString)
                        {
                            pedido.IdPedido = item.GetValue("ID_ENC").ToString();
                            pedido.NombreProducto = item.GetValue("PRODUCTO").ToString();
                            pedido.Local = item.GetValue("LOCAL").ToString();
                            pedido.Precio = item.GetValue("PRECIO").ToString();
                            pedido.EstadoPedido = item.GetValue("ESTADO_PEDIDO").ToString();
                            pedido.Cantidad = item.GetValue("CANTIDAD").ToString();
                            pedido.Total = item.GetValue("TOTAL").ToString();
                            pedido.TipoPago = item.GetValue("TIPO_PAGO").ToString();
                            pedido.Imagen = "sushi.jpg";
                            pedido.Observacion = item.GetValue("OBSERVACION").ToString();
                        }
                        await Navigation.PushModalAsync(new PaginaMaestraUsuario("DetallePedido", prod, pedido));
                    }
                    else
                    {
                        await DisplayAlert("Alerta", respuesta[1].ToString(), "OK");
                    }
                }
            };
            cmdCancelar.Clicked += async (sender, e) =>
            {
                await Navigation.PopModalAsync();
            };

            stack_cuatro.Children.Add(cmdSiguiente);
            stack_cuatro.Children.Add(cmdCancelar);
            //Definicion de Grilla 
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            // Agregar Stack uno y dos en Grilla
            grid.Children.Add(stack_uno, 0, 0);
            grid.Children.Add(stack_dos, 1, 0);
            // Agregar Elementos a stack General
            var stack_general = new StackLayout();
            var stack_general_total = new StackLayout { Margin = new Thickness(10, 20, 10, 0), BackgroundColor = Color.White };
            stack_general_total.Children.Add(grid);
            stack_general_total.Children.Add(stack_tres);

            stack_general.Children.Add(stack_general_total);
            stack_general.Children.Add(stack_cuatro);
            var contentView = new ContentView
            {
                Content = stack_general,
                // ControlTemplate = pruebatemplate,
                BackgroundColor = Color.FromHex("#E9E9E9")
            };
            Content = contentView;
        }

        public VistaPrevia(object prod)
        {
            this.prod = prod;
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            if (switcher.IsToggled)
            {
                switcher_dos.IsToggled = false;
            }
        }
        void switcher_Toggled_dos(object sender, ToggledEventArgs e)
        {
            if (switcher_dos.IsToggled)
            {
                switcher.IsToggled = false;
            }
        }
    }
}
