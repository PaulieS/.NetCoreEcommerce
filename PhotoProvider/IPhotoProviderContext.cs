using Microsoft.Data.Entity;

namespace PhotoProvider
{
    internal interface IPhotoProviderContext
    {
         DbSet<Photo> Photos { get; set; }

         DbSet<ProviderClient> Clients { get; set; } 
    }
}