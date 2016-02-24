using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultPhotoProvider
{
    public class Photo
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual ProviderClient Client{ get; set; }
    }
}