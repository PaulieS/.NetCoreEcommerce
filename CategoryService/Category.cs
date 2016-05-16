using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryService
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        [ForeignKey("ChildCategory")]
        public int ChildCategoryId { get; set; }
        public virtual Category ChildCategory { get; set; }
    }
}