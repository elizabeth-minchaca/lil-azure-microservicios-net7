using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lil.Search.Interfaces;
using Lil.Search.Services;

namespace Lil.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ICustomersService costumersService;
        private readonly IProductsService productsService;
        private readonly ISalesServices salesServices;

        public SearchController(ICustomersService costumersService, IProductsService productsService, ISalesServices salesServices)
        {
            this.costumersService = costumersService;
            this.productsService = productsService;
            this.salesServices = salesServices;
        }

        [HttpGet("customers/{customerId}")]
        public async Task<IActionResult> SearchAsync (string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                return BadRequest();
            }
            try
            {
                var customer = await this.costumersService.GetAsync(customerId);
                var sales = await this.salesServices.GetAsync(customerId);
                foreach ( var sale in sales)
                {
                    foreach (var item in sale.Items)
                    {
                        var product = await this.productsService.GetAsync(item.ProductId);
                        item.Product = product;
                    }
                }
                var result = new
                {
                    Customer = customer,
                    Sales = sales
                };
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
