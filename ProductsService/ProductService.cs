using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Internal;
using ProductsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.Entity.ChangeTracking;

[assembly: InternalsVisibleTo("Tests")]
namespace ProductsService
{
    public class ProductService : IDisposable 
    {
        public string ConnectionString { get; set; }

        private BaseProductsContext Db { get; set; }

        public ProductService(string connectionsString):this(new ProductsContext(connectionsString))
        {
            ConnectionString = connectionsString;
        }

        internal ProductService(BaseProductsContext db)
        {
            this.Db = db;
        }
        public async Task Initialise()
        {
            await Db.Database.EnsureCreatedAsync();
        }
        public async Task InitialiseWithSeedData()
        {
                await Db.Database.EnsureCreatedAsync();
                var seedData = SeedData.GetSampleProducts();
                Db.AddRange(seedData);
                seedData = null;
                await Db.SaveChangesAsync();
        }
        public async Task<Product> GetProduct(int id)
        {
            if (Db != null)
            {
                var product = await Db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (product != null)
                    return product;
                else
                    throw new ProductNotFoundException();
            }
            else
                throw new ProductsServiceNotInitialisedException();
        }
        public async Task<IEnumerable<Product>> GetProducts(int take, int skip)
        {
            if (Db != null)
                return await Db.Products.AsNoTracking().Take(take).Skip(skip).ToListAsync();
            else
                throw new ProductsServiceNotInitialisedException();
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
                Db.Entry(product).State = EntityState.Modified;
                await Db.SaveChangesAsync();
            }
            else
            {
                throw new ProductCantHaveNegativeStockException();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
                Db = null;
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ProductService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
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
