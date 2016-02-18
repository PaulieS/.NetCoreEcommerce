using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]
namespace PhotoProvider
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PhotoProvider
    {
        private PhotoProviderContext db;

        public PhotoProvider(string connectionString):this(new PhotoProviderContext(connectionString))
        {

        }

        public async Task Initialise()
        {
            await db.Database.EnsureCreatedAsync();
        }

        internal PhotoProvider(PhotoProviderContext db)
        {
            this.db = db;
        }

        

    }
}
