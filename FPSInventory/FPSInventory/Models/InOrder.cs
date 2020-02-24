using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class InOrder
    {
        public InOrder()
        {
            InItemOrder = new HashSet<InItemOrder>();
        }

        public int IdinOrder { get; set; }
        public int IdShippingCompany { get; set; }
        public int IdSupplier { get; set; }
        public int IdEmployee { get; set; }
        public DateTime? Date { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual ShippingCompany IdShippingCompanyNavigation { get; set; }
        public virtual Supplier IdSupplierNavigation { get; set; }
        public virtual ICollection<InItemOrder> InItemOrder { get; set; }
    }
}
