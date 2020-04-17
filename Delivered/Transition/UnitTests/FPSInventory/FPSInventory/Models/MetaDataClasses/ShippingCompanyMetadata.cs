using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(ShippingCompanyMetadata))]
    public partial class ShippingCompany : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Namecompany = Namecompany.Trim();
            Namecompany = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Namecompany.ToLower());

            if (Namecompany == "")
                yield return new ValidationResult("Company name is required",
                        new[] { nameof(Namecompany) });
            else if (Namecompany.Length > 30)
                yield return new ValidationResult("Company name cannot be longer than 30 characters",
                       new[] { nameof(Namecompany) });

            
            Address = Address.Trim();
            Address = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Address.ToLower());

            if (Address.Length > 255)
                yield return new ValidationResult("Address cannot be greater than 255 characters",
                        new[] { nameof(Address) });

            string phonenumber = "";

            foreach (char letter in Phonenumber)
            {
                if(char.IsDigit(letter))
                {
                    phonenumber += letter;
                }
            }

            if (phonenumber.Length != 10)
                yield return new ValidationResult("Phone Number must only have 10 digits",
                        new[] { nameof(Phonenumber) });
            else
            {
                phonenumber = phonenumber.Insert(0, "(");
                phonenumber = phonenumber.Insert(4, ") ");
                phonenumber = phonenumber.Insert(9, "-");
                Phonenumber = phonenumber;
            }

            var company = _context.ShippingCompany.FirstOrDefault(c => c.Namecompany.ToUpper() == Namecompany.ToUpper()
                                                        && c.Address.ToUpper() == Address.ToUpper());


            if (company != null && company.IdshippingCompany != IdshippingCompany)
                yield return new ValidationResult("That company is already on file: ID#" + company.IdshippingCompany,
                        new[] { nameof(Namecompany) });

        }
    }

    public class ShippingCompanyMetadata
    {
        public int IdshippingCompany { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Namecompany { get; set; }
        [Required]
        [Display(Name = "St Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string Phonenumber { get; set; }
        [Display(Name = "City")]
        public int IdCity { get; set; }
    }
}
