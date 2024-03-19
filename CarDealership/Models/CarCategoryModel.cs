using CarDealership.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarCategoryModel
    {
        [Required]
        [NotNullOrEmptyValidation]
        public string? Name { get; set; }
    }
}
