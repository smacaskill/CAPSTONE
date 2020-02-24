using System;
using System.Collections.Generic;

namespace FPSInventory.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InOrder = new HashSet<InOrder>();
            OutOrder = new HashSet<OutOrder>();
        }

        public int Idemployee { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }

        public virtual ICollection<InOrder> InOrder { get; set; }
        public virtual ICollection<OutOrder> OutOrder { get; set; }
    }
}
