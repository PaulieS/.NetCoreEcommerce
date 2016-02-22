using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

[assembly: InternalsVisibleTo("Tests")]
namespace PhotoProvider
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PhotoProvider
    {
        private PhotoProviderContext Db { get; set; }

        public PhotoProvider(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            Db = new PhotoProviderContext(optionsBuilder.Options);
        }
        public PhotoProvider(PhotoProviderContext db)
        {
            this.Db = db;
        }
        public async Task Initialise()
        {
            await Db.Database.EnsureCreatedAsync();
        }



        

    }
}
