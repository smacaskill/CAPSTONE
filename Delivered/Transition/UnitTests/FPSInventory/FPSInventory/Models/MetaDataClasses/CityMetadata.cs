using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(CityMetadata))]
    public partial class City : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();


            Namecity = Namecity.Trim();
            Namecity = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Namecity.ToLower());

            if (Namecity == "")
                yield return new ValidationResult("City Name is required",
                        new[] { nameof(Namecity) });
            else if (Namecity.Length > 30)
                yield return new ValidationResult("City Name cannot be more than 30 characters",
                        new[] { nameof(Namecity) });



            var city = _context.City.FirstOrDefault(c => c.Namecity.ToUpper() == Namecity.ToUpper()
                                            && c.Province.ToUpper() == Province.ToUpper());


            if (city != null && city.Idcity != Idcity)
                yield return new ValidationResult("That city is already on file: ID#" + city.Idcity,
                        new[] { nameof(Namecity) });

            

            Province = Province.Trim();
            Province = Province.ToUpper();

            if (Province == "")
                yield return new ValidationResult("Province is required",
                        new[] { nameof(Province) });
            else if(Province.Length > 2)
                yield return new ValidationResult("Province must be 2 characters only",
                        new[] { nameof(Province) });
        }
    }

    public class CityMetadata
    {
        public int Idcity { get; set; }
        [Required]
        [Display(Name = "City")]
        public string Namecity { get; set; }
        [Required]
        [Display(Name = "Province")]
        public string Province { get; set; }
    }
}
