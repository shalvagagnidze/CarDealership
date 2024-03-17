using AutoMapper;
using CarDealership.Data;
using CarDealership.Entities;
using CarDealership.Models;
using CarDealership.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CarDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly DbSet<User> _user;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(CarDbContext db, IMapper mapper,IAuthService authService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _authService = authService;
            _user = _db.Set<User>();
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("user-registration")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Registration(UserModel userModel)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _authService.Registration(userModel);
        }

        [HttpPost("user-login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login(string email,string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _authService.Login(email, password);
            
        }

    }
}
