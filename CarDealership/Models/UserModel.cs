using CarDealership.Entities;
using CarDealership.Entities.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class UserModel
    {

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [IdNumber]
        public string? IdNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PhoneValidation]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Role { get; set; }

    }
}
