using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(CustomerOrderMetadata))]
    public partial class CustomerOrder : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date > DateTime.Now)
                yield return new ValidationResult("Order date cannot be in the future",
                        new[] { nameof(Date) });
        }
    }

    public class CustomerOrderMetadata
    {
        [Display(Name = "Customer Order #")]
        public int IdcustomerOrder { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Display(Name = "Sold From")]
        public int? IdStore { get; set; }

    }
}
