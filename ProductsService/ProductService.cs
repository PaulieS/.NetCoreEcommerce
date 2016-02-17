using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Internal;
using ProductsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]
namespace ProductsService
{
    public class ProductService
    {
        public string ConnectionString { get; set; }

        private BaseProductsContext db { get; set; }

        public ProductService(string connectionsString):this(new ProductsContext(connectionsString))
        {
           
        }

        internal ProductService(BaseProductsContext db)
        {
            this.db = db;
        }
        public async Task Initialise()
        {
            await db.Database.EnsureCreatedAsync();
        }
        public async Task InitialiseWithSeedData()
        {
            await db.Database.EnsureCreatedAsync();
            db.AddRange(SeedData.GetSampleProducts());
                await db.SaveChangesAsync();
          
        }
        public async Task<Product> GetProduct(int id)
        {
            if (db!=null)
            {
                var product = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (product!=null)
                {
                    return product;
                }
                else
                {
                    throw new ProductNotFoundException();
                }
            }
            else
            {
                throw new ProductsServiceNotInitialisedException();
            }
        }
        public async Task<IEnumerable<Product>> GetProducts(int take, int skip)
        {
            if (db != null)
            {
                return await db.Products.AsNoTracking().Take(take).Skip(skip).ToListAsync();
            }
            else
            {
                throw new ProductsServiceNotInitialisedException();
            }
        }

        public async Task<decimal> GetProductStock(int productId)
        {
            var product = await GetProduct(productId);
            return product.Stock;
        }

        public async Task SubstractFromProductStock(int productId, decimal quantity)
        {
            var product = await GetProduct(productId);
            if (product.Stock>=quantity)
            {
                product.Stock = product.Stock - quantity;
                db.Products.
            }
        }
        //public async Task<List<Product>> GetProducts(int take, int skip, Func<Product, Key> orderBy)
        //{
        //    if (db != null)
        //    {
        //        return await db.Products.Take(take).Skip(skip).OrderBy(Prod.ToListAsync();
        //    }
        //    else
        //    {
        //        throw new ProductsServiceNotInitialisedException();
        //    }
        //}
    }
}
