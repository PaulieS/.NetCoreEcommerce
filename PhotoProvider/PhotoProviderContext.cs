using Microsoft.Data.Entity;
using System;
using Microsoft.Data.Entity.Infrastructure;

namespace DefaultPhotoProvider
{
    public class PhotoProviderContext : DbContext, IPhotoProviderContext
    {
        public DbSet<Photo> Photos { get; set; }
       
        public DbSet<ProviderClient> Clients { get; set; }

        public PhotoProviderContext(DbContextOptions options):base(options)
        {
        }

        public PhotoProviderContext()
        {
        }
    }
}