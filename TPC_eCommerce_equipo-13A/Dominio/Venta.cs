using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public enum PaymentMethod
    {
        Efectivo = 1,

        [Display(Name = "Tarjeta de crédito")]
        TarjetaCredito = 2,

        [Display(Name = "Tarjeta de débito")]
        TarjetaDebito = 3,

        [Display(Name = "Transferencia bancaria")]
        Transferencia = 4,

        MercadoPago = 5
    }

    public enum ShippingMethod
    {
        Retiro = 1,

        [Display(Name = "Envío")]
        Envio = 2
    }

    public enum OrderStatus
    {
        Pendiente = 0,

        [Display(Name = "En preparación")]
        EnPreparacion = 1,

        Enviado = 2,

        [Display(Name = "Listo para retirar")]
        ListoRetira = 3,

        Entregado = 4
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
        public OrderStatus EstadoPedido { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
