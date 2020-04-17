using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(ProductMetadata))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Product1 = Product1.Trim();

            Product1 = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Product1.ToLower());

            if (Product1 == "")
                yield return new ValidationResult("Description is required",
                        new[] { nameof(Product1) });
            else if (Product1.Length > 255)
                yield return new ValidationResult("Description cannot be longer than 255 Characters",
                        new[] { nameof(Product1) });

            var product = _context.Product.FirstOrDefault(a => a.Product1.ToUpper() == Product1.ToUpper());

            if (product != null && product.Idproduct != Idproduct)
                yield return new ValidationResult("A product with that description already exists",
                        new[] { nameof(Product1) });

        }
    }

    public class ProductMetadata
    {
        [Display(Name = "Product ID")]
        public int Idproduct { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Product1 { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int IdCategory { get; set; }
    }
}
