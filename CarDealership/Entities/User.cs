using CarDealership.Entities.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Entities
{
    public class User : IdentityUser
    {

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [IdNumber]    
        public string? IdNumber { get; set; }
        public List<Report>? Reports { get; set; }


    }
}
