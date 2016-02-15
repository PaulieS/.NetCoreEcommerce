using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Model
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        private string connectionString { get; set;  }
        public ProductsContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(new Npgsql.NpgsqlConnection(connectionString));
        }
    }
}
