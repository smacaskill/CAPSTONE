using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(InItemOrderMetadata))]
    public partial class InItemOrder : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            //Namecompany = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
            //    ToTitleCase(Namecompany.ToLower());

            if (Quantity < 1)
                yield return new ValidationResult("Quantity must be at least 1",
                        new[] { nameof(Quantity) });
            else if (Quantity > 100000)
                yield return new ValidationResult("Quantity cannot be greater than 100,000",
                        new[] { nameof(Quantity) });

            if (Price < 0)
                yield return new ValidationResult("Price cannot be less than 0, if free or promotional please enter 0",
                        new[] { nameof(Price) });
        }
    }

    public class InItemOrderMetadata
    {
        public int IdinItemOrder { get; set; }
        [Display(Name = "Product")]
        public int IdProduct { get; set; }
        [Display(Name = "Order #")]
        public int IdInorder { get; set; }
        [Required]
        [Display(Name = "Qty")]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
