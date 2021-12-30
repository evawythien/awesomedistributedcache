using AwesomeDistributed.Site.Caching;
using AwesomeDistributed.Site.Data;
using AwesomeDistributed.Site.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Services.Command
{
    public class CreateProduct
    {
        public class CreateProductsRequest : IRequest<CreateProductsResponse>, ICacheInvalidationRequest
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            public string GetCacheKey()
            {
                return $"{nameof(GetProductsMediatr)}_";
            }
        }

        public class CreateProductsResponse
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public bool Available { get; set; }

            public CreateProductsResponse(Product product)
            {
                this.Id = product.Id;
                this.Name = product.Name;
                this.Price = product.Price;
                this.Available = product.Available;
            }
        }

        public class CreateProductsCommandHandler : IRequestHandler<CreateProductsRequest, CreateProductsResponse>
        {
            private readonly AwesomeDistributedContext context;

            public CreateProductsCommandHandler(AwesomeDistributedContext context)
            {
                this.context = context;
            }

            public async Task<CreateProductsResponse> Handle(CreateProductsRequest request, CancellationToken cancellationToken)
            {
                Product product = new Product(request.Name, request.Price, true);
                this.context.Products.Add(product);

                await this.context.SaveChangesAsync();

                return new CreateProductsResponse(product);
            }
        }
    }
}
