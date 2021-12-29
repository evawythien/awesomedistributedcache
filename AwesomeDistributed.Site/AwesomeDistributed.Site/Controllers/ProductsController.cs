using AwesomeDistributed.Site.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeDistributed.Site.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService productsServices;

        public ProductsController(ProductsService productsServices)
        {
            this.productsServices = productsServices;
        }

        public ActionResult Get()
        {
            return null;
        }

        public ActionResult GetAll()
        {
            return null;
        }
    }
}
