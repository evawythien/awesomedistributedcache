using AwesomeDistributed.Site.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDistributed.Site.Data
{
    public class AwesomeDistributedContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AwesomeDistributedContext(DbContextOptions<AwesomeDistributedContext> options) : base(options) { }

    }
}
