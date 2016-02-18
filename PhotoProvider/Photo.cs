using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProvider
{
    public class Photo
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public PhotoSizeEnum Size { get; set; }

        public String Path { get; set; }

        public virtual ProviderClient Client{ get; set; }
    }
}