using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.Entities.Validations
{
    public class NotNullOrEmptyValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var variable = value as string;

            if (string.IsNullOrWhiteSpace(variable))
            {
                return new ValidationResult("Don't Skip The Required Field Or Enter Empty Spaces!");
            }
            return ValidationResult.Success;
        }
    }
}
