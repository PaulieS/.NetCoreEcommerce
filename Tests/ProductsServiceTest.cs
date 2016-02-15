using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    
    public class ProductsServiceTest
    {
        [Theory]
        [InlineData("Host=192.168.1.104; Database=ef7test; Username=postgres; Password=sexpistols1")]
        [InlineData("DUPA HEHEHE")]
        public void SimpleDatabaseInitialisation(string connectionString)
        {
            ProductsService.ProductService ps = new ProductsService.ProductService(connectionString);
            Task.Run(async () =>
            {
                await ps.Initialise();
            }).GetAwaiter().GetResult();
    
            Assert.True(true);
        }
    }
}
