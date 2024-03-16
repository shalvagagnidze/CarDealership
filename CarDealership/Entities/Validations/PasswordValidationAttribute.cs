using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.Entities.Validations
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (!string.IsNullOrEmpty(password))
            {
                if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[\W_0-9])(?=.*[a-z]).{8,}$"))

                {
                    return new ValidationResult("Password Must Contain At Least 8 Characters Including Upper Case Letter And Symbol");
                }
            }
            return ValidationResult.Success;
        }
    }
}
