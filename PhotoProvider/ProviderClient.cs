using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Entity.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultPhotoProvider
{
    public class ProviderClient
    {
        public int Id { get; set; }
    
        public Int32 ObjectTypeHash { get; set; }

        public int ObjectId { get; set; }

        public ICollection<Photo> Photos { get; set; }

    }
}