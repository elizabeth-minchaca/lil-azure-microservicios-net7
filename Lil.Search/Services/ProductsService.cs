using Lil.Search.Interfaces;
using Lil.Search.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Lil.Search.Services
{
    public class ProductsService: IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ProductsService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<Product> GetAsync(string id)
        {
            var client = httpClientFactory.CreateClient("productsService");
            var response = await client.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                //Obtenemos el contenido en cadena
                var content = await response.Content.ReadAsStringAsync();
                //Deserializamos en un objeto de tipo Product
                var product = JsonConvert.DeserializeObject<Product>(content);
                return product;
            }
            return null;
        }
    }
}
