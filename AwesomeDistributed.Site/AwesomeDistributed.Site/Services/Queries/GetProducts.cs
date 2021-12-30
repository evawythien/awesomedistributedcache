using AwesomeDistributed.Site.Data;
using AwesomeDistributed.Site.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Services
{
    public class GetProducts
    {
        public class GetProductsRequest : IRequest<List<GetProductsResponse>>
        {
            public string? Name { get; set; }
            public bool? Available { get; set; }
        }

        public class GetProductsResponse
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public bool Available { get; set; }

            public GetProductsResponse(Product product)
            {
                this.Id = product.Id;
                this.Name = product.Name;
                this.Price = product.Price;
                this.Available = product.Available;
            }
        }

        public class GetProductsCommandHandler : IRequestHandler<GetProductsRequest, List<GetProductsResponse>>
        {
            private readonly AwesomeDistributedContext context;

            public GetProductsCommandHandler(AwesomeDistributedContext context)
            {
                this.context = context;
            }

            public async Task<List<GetProductsResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
            {
                IQueryable<Product> query = this.context.Products.Take(20000);

                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(p => p.Name.Contains(request.Name));

                if (request.Available != null)
                    query = query.Where(p => p.Available == request.Available);

                return await query.Select(product => new GetProductsResponse(product)).ToListAsync(cancellationToken);
            }
        }
    }
}

