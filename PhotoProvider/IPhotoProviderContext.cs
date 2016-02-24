using Microsoft.Data.Entity;

namespace DefaultPhotoProvider
{
    internal interface IPhotoProviderContext
    {
         DbSet<Photo> Photos { get; set; }

         DbSet<ProviderClient> Clients { get; set; } 
    }
}