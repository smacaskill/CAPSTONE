using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(StoreMetadata))]
    public partial class Store : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Namestore = Namestore.Trim();
            Namestore = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Namestore.ToLower());

            if (Namestore == "")
                yield return new ValidationResult("Location name is required",
                        new[] { nameof(Namestore) });
            else if (Namestore.Length > 50)
                yield return new ValidationResult("Location name cannot be greater than 50 characters",
                        new[] { nameof(Namestore) });

            if (Address != null)
            {
                Address = Address.Trim();
                Address = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(Address.ToLower());

                if (Address.Length > 50)
                    yield return new ValidationResult("Address cannot be greater than 50 characters",
                            new[] { nameof(Address) });
            }

            var store = _context.Store.FirstOrDefault(c => c.Namestore.ToUpper() == Namestore.ToUpper()
                                                        && c.Address.ToUpper() == Address.ToUpper());


            if (store != null && store.Idstore != Idstore)
                yield return new ValidationResult("That location is already on file: ID#" + store.Idstore,
                        new[] { nameof(Namestore) });

        }
    }

    public class StoreMetadata
    {
        [Display(Name = "Store #")]
        public int Idstore { get; set; }
        [Required]
        [Display(Name = "Location Name")]
        public string Namestore { get; set; }
        [Display(Name = "St Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City")]
        public int IdCity { get; set; }
    }
}
