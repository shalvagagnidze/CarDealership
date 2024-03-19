using CarDealership.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Entities
{
    public class CarCategory
    {
        public int Id { get; set; }

        [Required]
        [NotNullOrEmptyValidation]   
        public string? Name { get; set; }
        public List<CarModel>? Models { get; set; } 
    }
}
