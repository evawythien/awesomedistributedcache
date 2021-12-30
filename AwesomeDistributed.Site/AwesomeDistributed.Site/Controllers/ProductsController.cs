using AwesomeDistributed.Site.Services;
using AwesomeDistributed.Site.Services.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "name", "available" }, Duration = 5000)]
        public async Task<List<GetProducts.GetProductsResponse>> GetAll([FromQuery] GetProducts.GetProductsRequest request)
        {
            return await this.mediator.Send(request).ConfigureAwait(false);
        }

        [HttpGet("mediatr")]
        public async Task<List<GetProductsMediatr.GetProductsMediatrResponse>> GetAllMediatr([FromQuery] GetProductsMediatr.GetProductsMediatrRequest request)
        {
            return await this.mediator.Send(request).ConfigureAwait(false);
        }

        [HttpPost("mediatr")]
        public async Task<CreateProduct.CreateProductsResponse> CreateProduct([FromBody] CreateProduct.CreateProductsRequest request)
        {
            return await this.mediator.Send(request).ConfigureAwait(false);
        }
    }
}
