using System;
using System.Collections.Generic;
using System.Text;

namespace Delysoft.Apps.Usuario.Pedido.Model
{
    public class PedidoViewModel
    {
        public string IdPedido { get; set; }
        public string NombreProducto { get; set; }
        public string Local { get; set; }
        public string Precio { get; set; }
        public string EstadoPedido { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }
        public string TipoPago { get; set; }
        public string Fecha { get; set; }
        public string Imagen { get; set; }
        public string Observacion { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
    }
}
