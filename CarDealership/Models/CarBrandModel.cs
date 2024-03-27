using CarDealership.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarBrandModel
    {
        public int Id { get; set; }

        [NotNullOrEmptyValidation]
        public string? Name { get; set; }
    }
}
