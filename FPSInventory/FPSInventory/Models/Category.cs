using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Idcategory { get; set; }
        public string Namecategory { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
