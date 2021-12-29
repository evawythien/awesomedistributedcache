using AwesomeDistributed.Site.Entities;
using AwesomeDistributed.Site.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService productsServices;

        public ProductsController(ProductsService productsServices)
        {
            this.productsServices = productsServices;
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "name", "available" }, Duration = 5000)]
        public async Task<List<Product>> GetAll([FromQuery] string? name = null, [FromQuery] bool? available = null)
        {
            return await this.productsServices.Get(name, available);
        }
    }
}
