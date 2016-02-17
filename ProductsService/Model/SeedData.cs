using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Model
{
    public static class SeedData
    {
        public static List<Product> GetSampleProducts()
        {
            var products = new List<Product>();
            for (int i = 0; i<10; i++)
			{
                products.Add(new Product()
                {
                    Name = $"Product no. {i}",
                    Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                                Praesent mauris sapien, mattis sed sapien et, pretium viverra ex.
                                Donec consectetur elementum ex, eu consectetur elit consectetur quis.
                                Quisque volutpat ullamcorper lectus vitae elementum. Integer nec elementum est. 
                                Vivamus consectetur molestie lorem sed suscipit. Pellentesque quam magna, tincidunt efficitur justo at, accumsan posuere elit. Mauris sit amet leo justo. In elementum sagittis ullamcorper.
                                Nam ut faucibus tortor. 
                                Curabitur pretium velit justo, quis pharetra odio hendrerit in.
                                Maecenas eget erat vitae velit dictum tincidunt non a diam. Aliquam mattis rutrum risus,
                                id efficitur nunc sagittis vitae. Pellentesque pulvinar risus quis justo iaculis, at imperdiet augue ullamcorper.
                                In volutpat arcu non eros fringilla, nec semper mi condimentum. 
                                Praesent metus lectus, ultricies ut massa ac, aliquet aliquam justo.",
                    GrossPrice = 10M,
                    NetPrice = 7.23M,
                    Stock = 100M
                });
            }
            return products;
        }
    }
}
