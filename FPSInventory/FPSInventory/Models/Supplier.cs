using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            InOrder = new HashSet<InOrder>();
        }

        public int Idsupplier { get; set; }
        public string Namesupplier { get; set; }
        public int IdCity { get; set; }
        public string Address { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<InOrder> InOrder { get; set; }
    }
}
