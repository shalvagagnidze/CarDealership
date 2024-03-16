using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.Entities.Validations
{
    public class IdNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var IdNumber = value as string;

            if (!string.IsNullOrEmpty(IdNumber))
            {
                if (!Regex.IsMatch(IdNumber, @"^[0-9]{11}$"))
                {
                    return new ValidationResult("ID Number Must Contain 11 Digits");
                }
            }
            return ValidationResult.Success;
        }
    }
}
