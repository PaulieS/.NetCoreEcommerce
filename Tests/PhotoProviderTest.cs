using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultPhotoProvider;
using ProductsService.Model;
using Xunit;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace Tests
{
    public class PhotoProviderTest
    {
        private PhotoProvider PhotoProviderWithRealDatabase
        {
            get
            {
                string connectionString = "Host=192.168.1.103; Database=ECommerceTest; Username=postgres; Password=password";
                return new DefaultPhotoProvider.PhotoProvider(connectionString,"images/");
            }
        }

        private PhotoProvider PhotoProviderWithInMemoryDatabase
        {
            get
            {
                var contextOptionsBuilder=  new DbContextOptionsBuilder();
                contextOptionsBuilder.UseInMemoryDatabase();

                var context = new PhotoProviderContext(contextOptionsBuilder.Options);
                return new PhotoProvider(context, "images/");
            }
        }
        [Fact]
        public void InitialisePhotoProvider()
        {
            DefaultPhotoProvider.PhotoProvider provider = PhotoProviderWithRealDatabase;
            Task.Run(async ()=>
            {
             await provider.Initialise();

            }).GetAwaiter().GetResult();
            Assert.True(true);
        }
        [Fact]
        public void AddObjectPhotoTest()
        {
            var photoProvider =  PhotoProviderWithInMemoryDatabase;
            byte[] photoToSave;
            using (var fileStream = File.Open(@"resources/img1.jpg", FileMode.Open, FileAccess.Read))
            {
                photoToSave = new byte[fileStream.Length];
                Task.Run(async () =>
                {
                    await fileStream.ReadAsync(photoToSave, 0, (int)fileStream.Length);
                }).GetAwaiter().GetResult();
            }
            Product product = new Product()
            {
                Id = 1,
                Name = "Product 1"
            };
            int photoId = 0;
            Task.Run(async () => photoId = await photoProvider.AddPhoto<Product>(photoToSave, product, (x) => x.Id, "jpg")).GetAwaiter().GetResult();
            byte[] savedPhoto;
            using (var savedFileStream = File.Open($"images/{photoId}.jpg", FileMode.Open, FileAccess.Read))
            {
                savedPhoto = new byte[savedFileStream.Length];
                Task.Run(async () =>
                {
                    await savedFileStream.ReadAsync(savedPhoto, 0, (int)savedFileStream.Length);
                }).GetAwaiter().GetResult();
            }

            Assert.Equal(photoToSave, savedPhoto);
        }

        [Fact]
        public void GetNonExistingProductPhoto()
        {
            var photoProvider = PhotoProviderWithInMemoryDatabase;
            Product product = new Product()
            {
                Id = 1,
                Name = "Product 1"
            };
            IEnumerable<Photo> photos = null;
            Task.Run(async () => photos = await photoProvider.GetPhotos<Product>(product, (x) => x.Id ) ).GetAwaiter().GetResult();
            Assert.True(!photos.Any());
        }
        [Fact]
        public void GetExistingProductPhoto()
        {
            var photoProvider = PhotoProviderWithInMemoryDatabase;
            byte[] photoToSave;
            using (var fileStream = File.Open(@"resources/img1.jpg", FileMode.Open, FileAccess.Read))
            {
                photoToSave = new byte[fileStream.Length];
                Task.Run(async () =>
                {
                    await fileStream.ReadAsync(photoToSave, 0, (int)fileStream.Length);
                }).GetAwaiter().GetResult();
            }
            Product product = new Product()
            {
                Id = 1,
                Name = "Product 1"
            };
            int photoId = 0;
            Task.Run(async () => photoId = await photoProvider.AddPhoto<Product>(photoToSave, product, (x) => x.Id, "jpg")).GetAwaiter().GetResult();
            IEnumerable<Photo> photos = null;
            Task.Run(async () => photos = await photoProvider.GetPhotos<Product>(product, (x) => x.Id)).GetAwaiter().GetResult();
            Assert.True(photos.Any());
        }

    }
}
