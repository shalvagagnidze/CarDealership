using System.ComponentModel.DataAnnotations;

namespace CarDealership.Entities
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int ManufactureYear { get; set; }
        public CarCategory? Category { get; set; }
        public CarBrand? Brand { get; set; }
        public List<Report>? Reports { get; set; }
    }
}
