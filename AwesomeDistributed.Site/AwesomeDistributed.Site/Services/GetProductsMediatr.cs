using AwesomeDistributed.Site.Caching;
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
    public class GetProductsMediatr
    {
        public class GetProductsMediatrRequest : IRequest<List<GetProductsMediatrResponse>>, ICacheableRequest
        {
            public string? Name { get; set; }
            public bool? Available { get; set; }

            public string GetCacheKey()
            {
                return $"{nameof(GetProductsMediatr)}_{Name}_{Available}";
            }
        }

        public class GetProductsMediatrResponse
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public bool Available { get; set; }

            public GetProductsMediatrResponse()
            {

            }

            public GetProductsMediatrResponse(Product product)
            {
                this.Id = product.Id;
                this.Name = product.Name;
                this.Price = product.Price;
                this.Available = product.Available;
            }
        }

        public class GetProductsMediatrCommandHandler : IRequestHandler<GetProductsMediatrRequest, List<GetProductsMediatrResponse>>
        {
            private readonly AwesomeDistributedContext context;

            public GetProductsMediatrCommandHandler(AwesomeDistributedContext context)
            {
                this.context = context;
            }

            public async Task<List<GetProductsMediatrResponse>> Handle(GetProductsMediatrRequest request, CancellationToken cancellationToken)
            {
                IQueryable<Product> query = this.context.Products.Take(1000);

                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(p => p.Name.Contains(request.Name));

                if (request.Available != null)
                    query = query.Where(p => p.Available == request.Available);

                return await query.Select(product => new GetProductsMediatrResponse(product)).ToListAsync(cancellationToken);
            }
        }
    }
}

