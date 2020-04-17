using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(OutOrderMetadata))]
    public partial class OutOrder : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date > DateTime.Now)
                yield return new ValidationResult("Order date cannot be in the future",
                        new[] { nameof(Date) });
        }
    }

    public class OutOrderMetadata
    {
        [Display(Name = "Store Order #")]
        public int IdoutOrder { get; set; }
        [Display(Name = "Shipping Company")]
        public int IdShippingCompany { get; set; }
        [Display(Name = "Store #")]
        public int IdStore { get; set; }
        [Display(Name = "Processed by")]
        public int IdEmployee { get; set; }
        [Required]
        public DateTime? Date { get; set; }
    }
}
