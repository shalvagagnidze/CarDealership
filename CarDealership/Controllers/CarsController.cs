using AutoMapper;
using CarDealership.Data;
using CarDealership.Entities;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDbContext _db;
        private readonly IMapper _mapper;
        public CarsController(CarDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("get-car-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> GetCarCategories()
        {
            var categories = await _db.Categories
                                      .Select(category => _mapper.Map<CarCategoryModel>(category))
                                      .ToListAsync();

            if (categories is null)
            {
                return Ok(new List<CarCategory>());
            }

            return Ok(categories);
        }

        [HttpGet("get-car-brand")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> GetCarBrands()
        {
            var brands = await _db.Brands
                                  .Select(brand => _mapper.Map<CarBrandModel>(brand))
                                  .ToListAsync();

            if (brands is null)
            {
                return Ok(new List<CarBrand>());
            }

            return Ok(brands);
        }

        [HttpGet("get-car-model")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> GetCarModels()
        {
            var models = await _db.Models
                                  .Select(model => new
                                  {
                                      model.Id,
                                      model.Name,
                                      model.ManufactureYear,                                    
                                      Caregory = new 
                                      {
                                           model.Category.Id,
                                           model.Category.Name
                                      },
                                      Brand = new 
                                      {
                                          model.Brand.Id,
                                          model.Brand.Name
                                      }
                                  })
                                  .ToListAsync();

            if (models is null)
            {
                return Ok(new List<CarModel>());
            }

            return Ok(models);
        }

        [HttpPost("add-car-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> AddCarCategory(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryExists = await _db.Categories.FirstOrDefaultAsync(category => category.Name == name);

            if (categoryExists is not null)
            {
                return BadRequest("Category Already Exists!");
            }
            var category = new CarCategory
            {
                Name = name,
            };

            await _db.Categories.AddAsync(category);

            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                return Ok("Category Has Added Successfully!");
            }

            return BadRequest();
        }

        [HttpPost("add-car-brand")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> AddCarBrand(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandExists = await _db.Brands.FirstOrDefaultAsync(brand => brand.Name == name);

            if (brandExists is not null)
            {
                return BadRequest("Brand Already Exists!");
            }
            var brand = new CarBrand
            {
                Name = name,
            };

            await _db.Brands.AddAsync(brand);

            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                return Ok("Brand Has Added Successfully!");
            }

            return BadRequest();
        }

        [HttpPost("add-car-model")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> AddCarModel(string name, int year, string brandName, string categoryName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelExists = await _db.Models.FirstOrDefaultAsync(model => model.Name == name);
            var brand = await _db.Brands.FirstOrDefaultAsync(brand => brand.Name == brandName);
            var category = await _db.Categories
                                    .FirstOrDefaultAsync(category => category.Name == categoryName);

            if (brand is null || category is null)
            {
                return NotFound("Brand Or Category Not Found!");
            }

            if (modelExists is not null)
            {
                return BadRequest("Model Already Exists!");
            }

            var model = new CarModel
            {
                Name = name,
                ManufactureYear = year,
                Brand = brand,
                Category = category
            };

            await _db.Models.AddAsync(model);

            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                return Ok("Model Has Added Successfully!");
            }

            return BadRequest();
        }

    }
}
