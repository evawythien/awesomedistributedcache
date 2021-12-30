using System.ComponentModel.DataAnnotations;

namespace AwesomeDistributed.Site.Entities
{
    public class Product
    {
        [Key]
        public long Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool Available { get; private set; }

        /// <summary>
        /// For entity framework.
        /// </summary>
        private Product()
        {
            this.Name = string.Empty;
        }

        public Product(string name, decimal price, bool available)
        {
            this.Name = name;
            this.Price = price;
            this.Available = available;
        }
    }
}
