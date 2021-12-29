using AwesomeDistributed.Site.Data;
using AwesomeDistributed.Site.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Services
{
    public class ProductsService
    {
        private readonly AwesomeDistributedContext context;

        public ProductsService(AwesomeDistributedContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> Get(string? name = null, bool? available = null)
        {
            IQueryable<Product> query = this.context.Products.Take(20000);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (available != null)
                query = query.Where(p => p.Available == available);

            return await query.ToListAsync();
        }
    }
}
