using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FPSInventory.Models
{
    [ModelMetadataTypeAttribute(typeof(EmployeeMetadata))]
    public partial class Employee : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            InventoryContext _context = new InventoryContext();

            Regex validEmail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                        + "@"
                                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            Name = Name.Trim();
            Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(Name.ToLower());

            if (Name == "")
                yield return new ValidationResult("Name is required",
                        new[] { nameof(Name) });
            else if (Name.Length > 30)
                yield return new ValidationResult("Name cannot be longer than 30 Characters",
                        new[] { nameof(Name) });

            Email = Email.Trim();

            var employee = _context.Employee.FirstOrDefault(a => a.Email.ToUpper() == Email.ToUpper());

            if (Email.Length > 50)
                yield return new ValidationResult("Email cannot be longer than 50 Characters",
                        new[] { nameof(Email) });

             else if (!validEmail.IsMatch(Email))
                yield return new ValidationResult("That is not a valid Email, use format \"aaa@yahoo.ca\"",
                        new[] { nameof(Email) });

            else if (employee != null && employee.Idemployee != Idemployee)
                yield return new ValidationResult(employee.Name + " is already using that email, please enter another.",
                        new[] { nameof(Email) });


            Gender = Gender.ToUpper().Trim();

            if (Gender.Length > 1 || (Gender != "M" && Gender != "F" && Gender != "X"))
                yield return new ValidationResult("Gender can only be M, F, or X",
                        new[] { nameof(Gender) });

            Role = Role.Trim().ToUpper();
            if (Role != "ADMIN" && Role != "USER")
                yield return new ValidationResult("Role must be either Admin or User",
                        new[] { nameof(Role) });
        }
    }

    public class EmployeeMetadata
    {
        public int Idemployee { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Gender (M, F, or X)")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Role (User or Admin)")]
        public string Role { get; set; }
    }
}
