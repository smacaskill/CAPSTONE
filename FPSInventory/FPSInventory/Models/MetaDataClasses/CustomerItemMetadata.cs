using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(CustomerItemMetadata))]
    public partial class CustomerItem : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            var storeOrder = _context.CustomerOrder.FirstOrDefault(a => a.IdcustomerOrder == IdCustomerOrder);
            
            var inOrders = _context.OutOrder.Where(a => a.IdStore == storeOrder.IdStore);

            int storeQuantity = 0;
            double averagePrice = 0;

            foreach (var order in inOrders)
            {
                var lineItems = _context.OutItemOrder
                    .Where(a => a.IdOutorder == order.IdoutOrder)
                    .Where(a => a.IdProduct == IdProduct);

                foreach (var item in lineItems)
                {
                    storeQuantity += item.Quantity;
                    averagePrice += item.Price * item.Quantity;
                }
            }

            averagePrice = Math.Round(averagePrice / storeQuantity, 2);

            var outOrders = _context.CustomerOrder.Where(a => a.IdStore == storeOrder.IdStore);

            foreach (var order in outOrders)
            {
                var lineItems = _context.CustomerItem
                    .Where(a => a.IdCustomerOrder == order.IdcustomerOrder)
                    .Where(a => a.IdProduct == IdProduct);

                foreach (var item in lineItems)
                {
                    storeQuantity -= item.Quantity;
                }
            }
            
            

            if (Quantity < 1)
                yield return new ValidationResult("Quantity must be at least 1",
                        new[] { nameof(Quantity) });
            else if (Quantity > storeQuantity)
                yield return new ValidationResult("That quantity would push store stock into negative quantity",
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

    public class CustomerItemMetadata
    {
        public int IdoutItemOrder { get; set; }
        [Display(Name = "Product")]
        public int IdProduct { get; set; }
        [Display(Name = "Customer Order #")]
        public int IdCustomerOrder { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
