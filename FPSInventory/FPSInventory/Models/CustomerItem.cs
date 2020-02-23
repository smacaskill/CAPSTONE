using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class CustomerItem
    {
        public int IdoutItemOrder { get; set; }
        public int IdProduct { get; set; }
        public int IdCustomerOrder { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public virtual CustomerOrder IdCustomerOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
