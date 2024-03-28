using CarDealership.Entities;
using CarDealership.Entities.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class UserModel
    {
        public int Id { get; set; }
       
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        [IdNumber]
        public string? IdNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [PhoneValidation]
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }

    }
}
