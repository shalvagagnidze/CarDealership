using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarModelDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int ManufactureYear { get; set; }
    }
}
