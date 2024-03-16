using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.Entities.Validations
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                if (!Regex.IsMatch(phoneNumber, @"^5[0-9]{8}$"))
                {
                    return new ValidationResult("Phone Number Must Contain Only Digits, Start with '5' And Contain 9 Digits");
                }
            }
            return ValidationResult.Success;
        }
    }
}
