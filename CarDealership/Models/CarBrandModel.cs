using CarDealership.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarBrandModel
    {
        [Required]
        [NotNullOrEmptyValidation]
        public string? Name { get; set; }
    }
}
