using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarModelDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        
        public int ManufactureYear { get; set; }
    }
}
