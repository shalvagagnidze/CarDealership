﻿using AutoMapper;
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
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(CarDbContext db, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet("get-users")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult GetUsers()
        {
            var query = from user in _db.Users
                        join userRole in _db.UserRoles
                        on user.Id equals userRole.UserId
                        join role in _db.Roles
                        on userRole.RoleId equals role.Id
                        select new
                        {
                            User = user,
                            Role = role
                        };

            var result = query.Select(o => new UserModel
            {
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,
                UserName = o.User.UserName,
                Email = o.User.Email,
                IdNumber = o.User.IdNumber,
                PhoneNumber = o.User.PhoneNumber,
                Role = o.Role.Name
            });
            
            return Ok(result);
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
                return Ok(new List<IdentityRole>());
            }

            return Ok(roles);
        }

        [HttpPost("add-roles")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(string role)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (!await _roleManager.RoleExistsAsync(role.ToUpper()))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            else
            {
                return BadRequest("That Role Already Exists!");
            }

            return Ok(roles);
        }

        [HttpPost("assign-role-to-user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleExists = await _roleManager.FindByNameAsync(role.ToUpper());

            if (user is null || roleExists is null)
            {
                return NotFound("User Or Role Doesn't Exist!");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return Ok("Role Assigned To The User Successfully");
        }

        [HttpPost("remove-role-from-user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleFromUser(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleExists = await _roleManager.FindByNameAsync(role.ToUpper());
            var userHasRole = await _userManager.IsInRoleAsync(user!, role);

            if (user is null || roleExists is null)
            {
                return NotFound("User Or Role Doesn't Exist!");
            }
            else
            {
                if (userHasRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                else
                {
                    return BadRequest("User Doesn't Have That Role");
                }

            }

            return Ok("Role Removed From The User Successfully");
        }

        [HttpPost("remove-all-roles-from-user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAllRolesFromUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user!);

            if (user is null || userRoles is null)
            {
                return NotFound("User Doesn't Exist Or Doesn't Have Any Role!");
            }
            else
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }

            return Ok("Roles Removed From The User Successfully");
        }
    }
}
