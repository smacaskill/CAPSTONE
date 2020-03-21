using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPSInventory.Models
{
    public partial class Product
    {
        public Product()
        {
            CustomerItem = new HashSet<CustomerItem>();
            InItemOrder = new HashSet<InItemOrder>();
            OutItemOrder = new HashSet<OutItemOrder>();
        }

        public int Idproduct { get; set; }
        [Display(Name = "Product Name")]
        public string Product1 { get; set; }
        public int IdCategory { get; set; }

        [Display(Name = "Category")]
        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<CustomerItem> CustomerItem { get; set; }
        public virtual ICollection<InItemOrder> InItemOrder { get; set; }
        public virtual ICollection<OutItemOrder> OutItemOrder { get; set; }
    }
}
