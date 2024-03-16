using CarDealership.Entities;
using CarDealership.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Services
{
    public interface IAuthService
    {
        Task<string> GenerateTokenString(IdentityUser user);
        Task<IActionResult> Login(string email,string password);
        Task<IActionResult> Registration(UserModel user);
    }
}
