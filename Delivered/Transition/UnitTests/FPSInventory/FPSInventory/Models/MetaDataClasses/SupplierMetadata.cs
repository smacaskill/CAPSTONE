using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(SupplierMetadata))]
    public partial class Supplier : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Namesupplier = Namesupplier.Trim();
            Namesupplier = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Namesupplier.ToLower());

            if (Namesupplier == "")
                yield return new ValidationResult("Supplier Name is required",
                        new[] { nameof(Namesupplier) });
            else if (Namesupplier.Length > 50)
                yield return new ValidationResult("Supplier Name cannot be greater than 50 characters",
                        new[] { nameof(Namesupplier) });

            Address = Address.Trim();
            Address = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Address.ToLower());

            if (Address == "")
                yield return new ValidationResult("St Address is required",
                        new[] { nameof(Address) });
            else if (Address.Length > 50)
                yield return new ValidationResult("St Address cannot be greater than 50 characters",
                        new[] { nameof(Address) });

            var supplier = _context.Supplier.FirstOrDefault(c => c.Namesupplier.ToUpper() == Namesupplier.ToUpper()
                                                        && c.Address.ToUpper() == Address.ToUpper());


            if (supplier != null && supplier.Idsupplier != Idsupplier)
                yield return new ValidationResult("That supplier is already on file: ID #" + supplier.Idsupplier,
                        new[] { nameof(Namesupplier) });
        }
    }

    public class SupplierMetadata
    {
        public int Idsupplier { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public string Namesupplier { get; set; }
        [Display(Name = "City")]
        public int IdCity { get; set; }
        [Required]
        [Display(Name = "St Address")]
        public string Address { get; set; }
    }
}
