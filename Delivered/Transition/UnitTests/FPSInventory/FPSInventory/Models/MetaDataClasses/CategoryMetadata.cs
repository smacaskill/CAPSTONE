using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class Category : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Namecategory = Namecategory.Trim();

            Namecategory = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Namecategory.ToLower());

            if (Namecategory == "")
                yield return new ValidationResult("A Name for this category is required",
                        new[] { nameof(Namecategory) });
            else if (Namecategory.Length > 30)
                yield return new ValidationResult("Name for this category cannot be longer than 30 Characters",
                        new[] { nameof(Namecategory) });

            var category = _context.Category.FirstOrDefault(a => a.Namecategory.ToUpper() == Namecategory.ToUpper());

            if (category != null && category.Idcategory != Idcategory)
                yield return new ValidationResult("A category with that Name already exists",
                        new[] { nameof(Namecategory) });

        }
    }

    public class CategoryMetadata
    {
        public int Idcategory { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Namecategory { get; set; }
    }
}
