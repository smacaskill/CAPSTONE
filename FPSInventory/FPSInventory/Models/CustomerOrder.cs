using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            CustomerItem = new HashSet<CustomerItem>();
        }

        public int IdcustomerOrder { get; set; }
        public DateTime? Date { get; set; }
        public int? IdStore { get; set; }

        public virtual Store IdStoreNavigation { get; set; }
        public virtual ICollection<CustomerItem> CustomerItem { get; set; }
    }
}
