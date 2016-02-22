using ProductsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ProductsService;
using Xunit;

namespace Tests
{
    
    public class ProductsServiceTest
    {
        private ProductsContext GetRealDbProductContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseNpgsql("Host=192.168.1.104; Database=ECommerceTest; Username=postgres; Password=password");
            ProductsContext context =
                 new ProductsContext(builder.Options);
            return context;
        }


        public ProductService InitialiseProductsServiceWithSampleData()
        {
            var context = GetRealDbProductContext();

            context.Database.EnsureDeleted();
            context.SaveChanges();

            ProductService ps = new ProductsService.ProductService(context);
            Task.Run(async () =>
            {
                await ps.InitialiseWithSeedData();
            }).GetAwaiter().GetResult();
            ps.Dispose();
            context = GetRealDbProductContext();
            ps = new ProductsService.ProductService(context);
            return ps;

        }
        [Fact]
        public void SimpleDatabaseInitialisation()
        {
            var context = GetRealDbProductContext();
            context.Database.EnsureDeleted();
            context.SaveChanges();

            ProductsService.ProductService ps = new ProductsService.ProductService(context);
            Task.Run(async () =>
            {
                await ps.Initialise();
            }).GetAwaiter().GetResult();
            var products = Task.Run(async () =>
            {
                return await ps.GetProducts(10, 0);
            }).GetAwaiter().GetResult();
            Assert.True(products.Count()== 0);
        }

        [Fact]
        public void DatabaseInitialisationWithSeedData()
        {
            using (var ps = InitialiseProductsServiceWithSampleData())
            {
                var products = Task.Run(async () => await ps.GetProducts(10, 0)).GetAwaiter().GetResult();
                Assert.True(products.Count() > 0);
            }
        }

        [Fact]
        public void GetExisistingProduct()
        {
            using (var ps = InitialiseProductsServiceWithSampleData())
            {
                var product = Task.Run(async () => await ps.GetProduct(1)).GetAwaiter().GetResult();
                Assert.True(product != null);
            }
        }

        [Fact]
        public void GetNotExistingProduct()
        {
            var ps = InitialiseProductsServiceWithSampleData();
          Assert.Throws<ProductNotFoundException>(()=>Task.Run(async () => await ps.GetProduct(-1)).GetAwaiter().GetResult());
        }
        [Fact]
        public void GetExistingProductStock()
        {
            using (var ps = InitialiseProductsServiceWithSampleData())
            {
                decimal stock = Task.Run(async () => await ps.GetProductStock(1)).GetAwaiter().GetResult();
                Assert.True(stock == 100);
            } 
   
        }

        [Fact]
        public void GetNotExistingProductStock()
        {
            using (var ps = InitialiseProductsServiceWithSampleData())
            {
                Assert.Throws<ProductNotFoundException>(
                    () => Task.Run(async () => await ps.GetProductStock(-1)).GetAwaiter().GetResult());
            }
        }

        [Fact]
        public void SubstractFromExistingProductStockWhereSubstractedValueIsLessThanStock()
        {
            using (var ps = InitialiseProductsServiceWithSampleData())
            {
            Task.Run(async () => await ps.SubstractFromProductStock(1, 10)).GetAwaiter().GetResult();
            decimal stock = Task.Run(async () => await ps.GetProductStock(1)).GetAwaiter().GetResult();
            Assert.True(stock == 90);
            }
        }
        [Fact]
        public void SubstractFromExistingProductStockWhereSubstractedValueIsHigherThanStock()
            {
                using (var ps = InitialiseProductsServiceWithSampleData())
                {
                    Assert.Throws<ProductCantHaveNegativeStockException>(
                        () => Task.Run(async () => await ps.SubstractFromProductStock(1,1000) ).GetAwaiter().GetResult());
                }
            }
        }
}
