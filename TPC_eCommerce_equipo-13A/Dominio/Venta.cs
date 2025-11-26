using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum PaymentMethod
    {
        Efectivo = 1,
        TarjetaCredito = 2,
        TarjetaDebito = 3,
        Transferencia = 4,
        MercadoPago = 5
    }

    public enum ShippingMethod
    {
        Retiro = 1,
        Envio = 2
    }

    public class Venta
    {
        public int Id { get; set; }                         //PK
        public Usuario Usuario { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public PaymentMethod MetodoPago { get; set; }
        public ShippingMethod MetodoEnvio { get; set; }
        public DateTime FechaHoraVenta { get; set; }
        public DateTime? FechaHoraEntrega { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
