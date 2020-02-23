using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class OutItemOrder
    {
        public int IdoutItemOrder { get; set; }
        public int IdProduct { get; set; }
        public int IdOutorder { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public virtual OutOrder IdOutorderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
