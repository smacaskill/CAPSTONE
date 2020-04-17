using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class InItemOrder
    {
        public int IdinItemOrder { get; set; }
        public int IdProduct { get; set; }
        public int IdInorder { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public virtual InOrder IdInorderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
