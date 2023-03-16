using Lil.Search.Interfaces;
using Lil.Search.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Lil.Search.Services
{
    public class SalesService : ISalesServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        public SalesService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<ICollection<Order>> GetAsync(string customerId)
        {
            var client = httpClientFactory.CreateClient("salesService");
            var response = await client.GetAsync($"api/sales/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                //Obtenemos el contenido en cadena
                var content = await response.Content.ReadAsStringAsync();
                //Deserializamos en un objeto de tipo ICollection de tipo Order
                var orders = JsonConvert.DeserializeObject<ICollection<Order>>(content);
                return orders;
            }
            return null;
        }
    }
}
