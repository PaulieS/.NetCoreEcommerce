using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Entity.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProvider
{
    public class ProviderClient
    {
        public int Id { get; set; }
    
        public int ObjectTypeId { get; set; }

        public int ObjectId { get; set; }

        private IEnumerable<Photo> Photos { get; set; }

    }
}