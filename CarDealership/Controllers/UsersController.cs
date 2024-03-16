using AutoMapper;
using CarDealership.Data;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public UsersController(CarDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users.Select(user => _mapper.Map<UserModel>(user)).ToListAsync();

            if(users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        
    }
}
