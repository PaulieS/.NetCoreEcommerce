using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
[assembly: InternalsVisibleTo("Tests")]
namespace CategoryService
{
    public class CategoryService
    {
        private readonly ProductsCategoryServiceContext _db;

        public CategoryService(string connectionsString)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionsString);
            _db = new ProductsCategoryServiceContext(optionsBuilder.Options);
        }

        internal CategoryService(ProductsCategoryServiceContext db)
        {
           _db = db;
        }
        public async Task Initialise()
        {
            await _db.Database.MigrateAsync();
        }
    }
}
