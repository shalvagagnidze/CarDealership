using CarDealership.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarCategoryModel
    {
        public int Id { get; set; }


        [NotNullOrEmptyValidation]
        public string? Name { get; set; }
    }
}
