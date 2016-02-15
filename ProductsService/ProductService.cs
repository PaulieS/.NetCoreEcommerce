using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Internal;
using ProductsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService
{
    public class ProductService
    {
        public string ConnectionString { get; set; }

        private ProductsContext db { get; set; }
        public ProductService(string connectionsString)
        {
            this.ConnectionString = connectionsString;
        }
        public async Task Initialise()
        {
            db = new ProductsContext(ConnectionString);
            await db.Database.EnsureCreatedAsync();
        }
        public async Task InitialiseWithSeedData()
        {
            db = new ProductsContext(ConnectionString);

            await db.Database.EnsureCreatedAsync();
            db.AddRange(SeedData.GetSampleProducts());
                await db.SaveChangesAsync();
          
        }
        public async Task<Product> GetProduct(int id)
        {
            if (db!=null)
            {
                var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<List<Product>> GetProducts(int take, int skip)
        {
            if (db != null)
            {
                return await db.Products.Take(take).Skip(skip).ToListAsync();
            }
            else
            {
                throw new ProductsServiceNotInitialisedException();
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
