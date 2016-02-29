using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultPhotoProvider;
using Microsoft.Data.Entity;
using ProductsService;
using ProductsService.Model;
using Xunit;

namespace Tests
{

    public class IntegrationTests
    {
 
        [Fact]
        public void CreateProductServiceAndPhotoProviderInSameDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql("Host=192.168.1.103; Database=ECommerceTest; Username=postgres; Password=password");

            PhotoProviderContext photoProviderContext = new PhotoProviderContext(builder.Options);
            PhotoProvider photoProvider = new PhotoProvider(photoProviderContext, "images/");
            Task.Run(async () =>
            {
                await photoProvider.Initialise();
            }).GetAwaiter().GetResult();
            ProductsContext productsContext =
                   new ProductsContext(builder.Options);

            var productService = new ProductService(productsContext);
            Task.Run(async () => { await productService.Initialise(); }).GetAwaiter().GetResult();
      
        }
    }
}
