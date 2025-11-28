using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Newtonsoft.Json;
using Dominio;

namespace Mercado_Pago
{
    public class MercadoPagoNegocio
    {
        private PreferenceRequest request;
        private List<PreferenceItemRequest> listaPreferenceItemRequest;
        private Preference preference;
        private PreferenceClient client;
        private string success;
        private string failure;
        private string pending;

        public Preference Preferencia
        {
            get { return preference; }
        }

        public MercadoPagoNegocio(string urlBase)
        {
            MercadoPagoConfig.AccessToken = ConfigurationManager.AppSettings["MERCADO_PAGO_TOKEN"];
            //202505: se agrega el reemplazo de http por https para las url de regreso.
            string baseUrl = urlBase.TrimEnd('/').Replace("http://", "https://");
            success = baseUrl + "/CompraExitosa.aspx";
            failure = baseUrl + "/CompraError.aspx";
            pending = baseUrl + "/CompraPendiente.aspx";
        }

        public string PagarMercadoPago(Producto producto)
        {
            try
            {
                CargarItemsPreferenceRequest(producto);
                CrearPreferenceRequest();

                client = new PreferenceClient();
                preference = client.Create(request);
                return preference.SandboxInitPoint;
            }
            catch (Exception ex)
            {
                //se puede guardar un log del error obtenido para analizar en detalle el problema.
                throw new Exception("Error al Pagar con Mercado Pago", ex);
            }
        }

        private void CrearPreferenceRequest()
        {
            //Armamos la referencia externa que se envía a Mercado Pago. 
            //Acá lo ideal sería que sea un Id de compra interno de nuestro sistema, ya que en otro escenario 
            //vamos a querer identificar a qué operación pertenece determinado pago.
            string externalReference = string.Concat("Compra-", DateTime.Now);
            request = new PreferenceRequest
            {
                Items = listaPreferenceItemRequest,
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = success,
                    Failure = failure,
                    Pending = pending
                },
                AutoReturn = "approved",
                ExternalReference = externalReference

            };

        }

        private void CargarItemsPreferenceRequest(Producto producto)
        {
            try
            {
                listaPreferenceItemRequest = new List<PreferenceItemRequest>();
                listaPreferenceItemRequest.Add(new PreferenceItemRequest
                {
                    Title = producto.Nombre,
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = producto.Precio
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los productos a la Preferencia Mercado Pago", ex);
            }
        }
    }
}
