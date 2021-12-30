using AwesomeDistributed.Site.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDistributed.Site.Data
{
    public class AwesomeDistributedContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        public AwesomeDistributedContext(DbContextOptions<AwesomeDistributedContext> options) : base(options) { }

    }
}
