using System.ComponentModel.DataAnnotations;

namespace AwesomeDistributed.Site.Entities
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
    }
}
