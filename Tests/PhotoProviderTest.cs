using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsService.Model;
using Xunit;

namespace Tests
{
    public class PhotoProviderTest
    {
        [Fact]
        public void InitialisePhotoProvider()
        {
            string connectionString =
                "Host=192.168.1.104; Database=ECommerceTest; Username=postgres; Password=password";
            PhotoProvider.PhotoProvider provider = new PhotoProvider.PhotoProvider(connectionString);
            Task.Run(async ()=>
            {
             await provider.Initialise();

            }).GetAwaiter().GetResult();
            Assert.True(true);
        }
        //[Fact]
        //public void AddObjectPhotoTest()
        //{
        //    Image photo = Resources.SamplePhotoLg;
        //    Product product;
        //    var photoId = PhotoProvider.AddPhoto<Product>(photo,product,(product)=> product.Id);
        //}
    }
}
