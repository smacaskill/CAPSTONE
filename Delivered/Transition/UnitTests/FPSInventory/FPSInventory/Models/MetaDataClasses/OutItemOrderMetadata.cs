using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(OutItemOrderMetadata))]
    public partial class OutItemOrder : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            var inproducts = _context.InItemOrder.Where(a => a.IdProduct == IdProduct);
            int totalQuantity = 0;
            double averagePrice = 0;

            foreach (var item in inproducts)
            {
                totalQuantity += item.Quantity;
                averagePrice += item.Price * item.Quantity;
            }

            averagePrice = Math.Round(averagePrice / totalQuantity, 2);

            var outproducts = _context.OutItemOrder.Where(a => a.IdProduct == IdProduct);

            foreach (var item in outproducts)
            {
                totalQuantity -= item.Quantity;
            }

            if (Quantity < 1)
                yield return new ValidationResult("Quantity must be at least 1",
                        new[] { nameof(Quantity) });
            else if (Quantity > totalQuantity)
                yield return new ValidationResult("Not enough inventory on-hand to send that quantity",
                        new[] { nameof(Quantity) });
            else if (Quantity > 100000)
                yield return new ValidationResult("Quantity cannot be greater than 100,000",
                        new[] { nameof(Quantity) });

            if (Price < 0)
                yield return new ValidationResult("Price cannot be less than 0, if free or promotional please enter 0",
                        new[] { nameof(Price) });
            else if (Price < averagePrice)
                yield return new ValidationResult("Price must be greater than purchase average price of: $" + averagePrice,
                        new[] { nameof(Price) });
        }
    }

    public class OutItemOrderMetadata
    {
        public int IdoutItemOrder { get; set; }
        [Display(Name = "Product")]
        public int IdProduct { get; set; }
        [Display(Name = "Store Order #")]
        public int IdOutorder { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
