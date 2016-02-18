using Microsoft.Data.Entity;
using System;

namespace PhotoProvider
{
    internal class PhotoProviderContext : DbContext, IPhotoProviderContext
    {
        public DbSet<Photo> Photos { get; set; }
       
        public DbSet<ProviderClient> Clients { get; set; }
        private string connectionString { get; set; }

        public PhotoProviderContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(new Npgsql.NpgsqlConnection(connectionString));
        }
    }
}