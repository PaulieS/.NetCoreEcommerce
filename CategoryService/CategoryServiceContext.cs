using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Internal;

namespace CategoryService
{
    public class ProductsCategoryServiceContext: DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public ProductsCategoryServiceContext(DbContextOptions options):base(options)
        {
        }

        public ProductsCategoryServiceContext()
        {
        }
    }
}
