using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(InOrderMetadata))]
    public partial class InOrder : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            if (Date > DateTime.Now)
                yield return new ValidationResult("Order date cannot be in the future",
                        new[] { nameof(Date) });
            
        }
    }

    public class InOrderMetadata
    {
        [Display(Name = "Order #")]
        public int IdinOrder { get; set; }
        [Display(Name = "Shipping Company")]
        public int IdShippingCompany { get; set; }
        [Display(Name = "Supplier")]
        public int IdSupplier { get; set; }
        [Display(Name = "Employee")]
        public int IdEmployee { get; set; }
        [Required]
        public DateTime? Date { get; set; }
    }
}
