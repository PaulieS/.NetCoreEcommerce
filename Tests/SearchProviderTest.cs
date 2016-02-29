using ProductsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class SearchProviderTest
    {
        /// <summary>
        /// Every product has one match 
        /// </summary>
        [Fact]
        public void FindMatchingProducts()
        {
            #region InputData
                        var products = new Product[]
                        {
                            new Product()
                            {
                                Description = "Wysokiej jakości papier kancelaryjny",
                                GrossPrice = 9.99M,
                                Id = 1,
                                Name = "kancelaryjny ryza 50szt"
                            }
                            , new Product()
                            {
                                GrossPrice = 9.99M,
                                Description = "ścierny do ścierania wszystkiego",
                                Id = 2,
                                Name = "Papier ścierny 5cmx15cm",
                                NetPrice = 9.99M,
                                Stock = 100
                            },
                            //new Product()
                            //{
                            //    Name = "Papierowa toreba na zakupy 50szt",
                            //    Description = "Wytrzymała torba na zakupy",
                            //    GrossPrice = 9.99M,
                            //    Id = 3,
                            //    NetPrice = 9.99M,
                            //    Stock = 1000

                            //},
                            new Product()
                            {
                               Id = 4,
                               Description = "Ołowek karton 100szt",
                               GrossPrice = 20M,
                               Name = "Ołowek",
                               NetPrice = 9.99M,
                               Stock = 1000
                            }
                        };
                        #endregion
            #region ProperOutput
                        var properResult = new Product[]
                      {
                            new Product()
                            {
                                Description = "Wysokiej jakości papier kancelaryjny",
                                GrossPrice = 9.99M,
                                Id = 1,
                                Name = "kancelaryjny ryza 50szt"
                            }
                            , new Product()
                            {
                                GrossPrice = 9.99M,
                                Description = "ścierny do ścierania wszystkiego",
                                Id = 2,
                                Name = "Papier ścierny 5cmx15cm",
                                NetPrice = 9.99M,
                                Stock = 100
                            }
                      };
            #endregion

            var searchProvider = new SearchProvider.SearchProvider();
            IQueryable<Product> matches = searchProvider.Search(products.AsQueryable(), "Papier", x => x.Name, x => x.Description);
            Assert.True(matches.OrderBy(x => x.Id).Select(x=>x.Id).ToList().SequenceEqual(properResult.OrderBy(x => x.Id).Select(x=>x.Id)));
        }
        /// <summary>
        /// Every product has two matchesS
        /// </summary>
        [Fact]
        public void FindMatchingProducts2()
        {
            #region InputData
            var products = new Product[]
            {
                            new Product()
                            {
                                Description = "Wysokiej jakości papier kancelaryjny",
                                GrossPrice = 9.99M,
                                Id = 1,
                                Name = "Papier kancelaryjny ryza 50szt"
                            }
                            , new Product()
                            {
                                GrossPrice = 9.99M,
                                Description = "Papier ścierny do ścierania wszystkiego",
                                Id = 2,
                                Name = "Papier ścierny 5cmx15cm",
                                NetPrice = 9.99M,
                                Stock = 100
                            },
                            //new Product()
                            //{
                            //    Name = "Papierowa toreba na zakupy 50szt",
                            //    Description = "Wytrzymała torba na zakupy",
                            //    GrossPrice = 9.99M,
                            //    Id = 3,
                            //    NetPrice = 9.99M,
                            //    Stock = 1000

                            //},
                            new Product()
                            {
                               Id = 4,
                               Description = "Ołowek karton 100szt",
                               GrossPrice = 20M,
                               Name = "Ołowek",
                               NetPrice = 9.99M,
                               Stock = 1000
                            }
            };
            #endregion
            #region ProperOutput
            var properResult = new Product[]
          {
                            new Product()
                            {
                                Description = "Wysokiej jakości papier kancelaryjny",
                                GrossPrice = 9.99M,
                                Id = 1,
                                Name = "Papier kancelaryjny ryza 50szt"
                            }
                            , new Product()
                            {
                                GrossPrice = 9.99M,
                                Description = "Papier ścierny do ścierania wszystkiego",
                                Id = 2,
                                Name = "Papier ścierny 5cmx15cm",
                                NetPrice = 9.99M,
                                Stock = 100
                            }
          };
            #endregion

            var searchProvider = new SearchProvider.SearchProvider();
            IQueryable<Product> matches = searchProvider.Search(products.AsQueryable(), "Papier", x => x.Name, x => x.Description);
            Assert.True(matches.OrderBy(x => x.Id).Select(x => x.Id).ToList().SequenceEqual(properResult.OrderBy(x => x.Id).Select(x => x.Id)));
        }
        /// <summary>
        /// Find no matching products
        /// </summary>
        [Fact]
        public void FindNoMatchingProducts()
        {
            #region InputData
            var products = new Product[]
            {
                            new Product()
                            {
                               Id = 4,
                               Description = "Ołowek karton 100szt",
                               GrossPrice = 20M,
                               Name = "Ołowek",
                               NetPrice = 9.99M,
                               Stock = 1000
                            }
            };
            #endregion
            #region ProperOutput
            var properResult = new Product[]{};
            #endregion

            var searchProvider = new SearchProvider.SearchProvider();
            IQueryable<Product> matches = searchProvider.Search(products.AsQueryable(), "Papier", x => x.Name, x => x.Description);
            Assert.True(matches.Count() == 0);
        }
        /// <summary>
        /// Search in empty collection
        /// </summary>
        [Fact]
        public void SearchInEmptyCollection()
        {
            #region InputData
            var products = new Product[]
            {
            };
            #endregion
            #region ProperOutput
            var properResult = new Product[] { };
            #endregion

            var searchProvider = new SearchProvider.SearchProvider();
            IQueryable<Product> matches = searchProvider.Search(products.AsQueryable(), "Papier", x => x.Name, x => x.Description);
            Assert.True(matches.Count() == 0);
        }
    }
}
