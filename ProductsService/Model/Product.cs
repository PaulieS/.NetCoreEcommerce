using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Model
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public decimal NetPrice { get; set; }

        public decimal GrossPrice { get; set; }

    }
}
