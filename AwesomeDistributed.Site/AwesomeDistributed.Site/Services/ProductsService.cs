using AwesomeDistributed.Site.Data;
using AwesomeDistributed.Site.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public List<Product> Get()
        {
            return this.context.Products.ToList();
        }

        public ActionResult GetAll()
        {
            return null;
        }
    }
}
