using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

[assembly: InternalsVisibleTo("Tests")]
namespace DefaultPhotoProvider
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PhotoProvider
    {
        private PhotoProviderContext Db { get; set; }
        private string imagesPath { get; set; }
        public PhotoProvider(string connectionString, string imagesPath)
        {
            this.imagesPath = imagesPath;
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            Db = new PhotoProviderContext(optionsBuilder.Options);
        }
        public PhotoProvider(PhotoProviderContext db, string imagesPath)
        {
            this.Db = db;
            this.imagesPath = imagesPath;

        }
        public async Task Initialise()
        {
            await Db.Database.MigrateAsync();
        }

        private Task<Int32> GetClientHash<T>(T client, Func<T, int> clientId)
        {
            return Task.Run(() =>
            {
                var objectType = typeof (T).AssemblyQualifiedName;
                return objectType.GetHashCode();
            });
        }
        private async Task<ProviderClient> GetClient<T>(T client, Func<T, int> clientId)
        {
            var typeHash = await GetClientHash(client, clientId);
            return await Db.Clients.FirstOrDefaultAsync(x => x.ObjectId == clientId(client) && x.ObjectTypeHash == typeHash);
        }

        private async Task<ProviderClient> CreateNewClient<T>(T client, Func<T, int> clientId)
        {
            var clientHash = await GetClientHash(client, clientId);
            var newClient = new ProviderClient()
            {
                ObjectId = clientId(client),
                ObjectTypeHash = clientHash
            };
            Db.Clients.Add(newClient);
            await Db.SaveChangesAsync();
            return newClient;
        }
        public async Task<int> AddPhoto<T>(byte[] file, T owner, Func<T, int> ownerId, string extension)
        {
            var client = await GetClient(owner, ownerId) ?? await CreateNewClient(owner, ownerId);
            var photoEntity = await AddPhotoToClient(client);
            await SavePhotoFile(photoEntity, file, extension);
            return photoEntity.Id;
        }

        private async Task SavePhotoFile(Photo photo, byte[] file, string extension)
        {
            var fileName = $"{photo.Id}.{extension}";
            var fileUri = Path.Combine(imagesPath, fileName);
            if (!Directory.Exists(fileUri))
            {
                Directory.CreateDirectory(imagesPath);
            }


            using (var fileStream = File.Create(fileUri))
            {
                await fileStream.WriteAsync(file, 0, file.Length);
            }
            photo.Path = fileUri;
            await Db.SaveChangesAsync();
        }
        private async Task<Photo> AddPhotoToClient(ProviderClient client)
        {
            Photo photo = new Photo {ClientId = client.Id};
            Db.Photos.Add(photo);
            await Db.SaveChangesAsync();
            return photo;
        }

        public async Task<IEnumerable<Photo>> GetPhotos<T>(T client, Func<T, int> clientId)
        {
            var clientHashCode = await GetClientHash(client, clientId);
            var clientEntity = Db.Clients.FirstOrDefault(x => x.ObjectTypeHash == clientHashCode && x.ObjectId == clientId(client));
            if(clientEntity==null) return Enumerable.Empty<Photo>();

            return clientEntity.Photos?? Enumerable.Empty<Photo>();

        }
    }
}
