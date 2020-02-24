using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class City
    {
        public City()
        {
            ShippingCompany = new HashSet<ShippingCompany>();
            Store = new HashSet<Store>();
            Supplier = new HashSet<Supplier>();
        }

        public int Idcity { get; set; }
        public string Namecity { get; set; }
        public string Province { get; set; }

        public virtual ICollection<ShippingCompany> ShippingCompany { get; set; }
        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
