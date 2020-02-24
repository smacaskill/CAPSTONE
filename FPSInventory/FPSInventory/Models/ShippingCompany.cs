using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class ShippingCompany
    {
        public ShippingCompany()
        {
            InOrder = new HashSet<InOrder>();
            OutOrder = new HashSet<OutOrder>();
        }

        public int IdshippingCompany { get; set; }
        public string Namecompany { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public int IdCity { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<InOrder> InOrder { get; set; }
        public virtual ICollection<OutOrder> OutOrder { get; set; }
    }
}
