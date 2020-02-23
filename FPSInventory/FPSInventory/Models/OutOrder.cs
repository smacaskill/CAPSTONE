using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class OutOrder
    {
        public OutOrder()
        {
            OutItemOrder = new HashSet<OutItemOrder>();
        }

        public int IdoutOrder { get; set; }
        public int IdShippingCompany { get; set; }
        public int IdStore { get; set; }
        public int IdEmployee { get; set; }
        public DateTime? Date { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual ShippingCompany IdShippingCompanyNavigation { get; set; }
        public virtual Store IdStoreNavigation { get; set; }
        public virtual ICollection<OutItemOrder> OutItemOrder { get; set; }
    }
}
