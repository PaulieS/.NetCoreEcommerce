using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Model
{
    public interface IProductsContext 
    {
        DbSet<Product> Products { get; set; }
    }
}
