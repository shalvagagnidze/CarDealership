using AutoMapper;
using CarDealership.Data;
using CarDealership.Entities;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CarDbContext _db;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(CarDbContext db, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _roleManager = roleManager;
        }


        [HttpGet("get-users")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users.Select(user => _mapper.Map<UserModel>(user)).ToListAsync();

            if(users is null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("get-roles")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (roles is null)
            {
                return NotFound();
            }

            return Ok(roles);
        }
    }
}
