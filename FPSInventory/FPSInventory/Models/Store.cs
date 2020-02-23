using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class Store
    {
        public Store()
        {
            CustomerOrder = new HashSet<CustomerOrder>();
            OutOrder = new HashSet<OutOrder>();
        }

        public int Idstore { get; set; }
        public string Namestore { get; set; }
        public string Address { get; set; }
        public int IdCity { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
        public virtual ICollection<OutOrder> OutOrder { get; set; }
    }
}
