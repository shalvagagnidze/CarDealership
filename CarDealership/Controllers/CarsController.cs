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
        public IActionResult GetCarCategories()
        {
            var categories = _mapper.Map<List<CarCategoryModel>>(_db.Categories);
                                      

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
        public IActionResult GetCarBrands()
        {
            var brands = _mapper.Map<List<CarCategoryModel>>(_db.Brands);
                                 
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
                                          model.Category!.Id,
                                          model.Category.Name
                                      },
                                      Brand = new
                                      {
                                          model.Brand!.Id,
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

        [HttpGet("get-car-model-by-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> GetCarModelsByCategory(string categoryName)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name!.ToUpper() == categoryName.ToUpper());

            if (category is null)
            {
                return NotFound("Category Not Found");
            }

            var models = await _db.Models
                                  .Where(model => model.Category!.Name!.ToUpper() == category.Name!.ToUpper())
                                  .Select(model => new
                                  {
                                      model.Id,
                                      model.Name,
                                      model.ManufactureYear,
                                      Brand = new
                                      {
                                          model.Brand!.Id,
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

        [HttpGet("get-car-model-by-brand")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> GetCarModelsByBrand(string bradnName)
        {
            var brand = await _db.Brands.FirstOrDefaultAsync(c => c.Name!.ToUpper() == bradnName.ToUpper());

            if (brand is null)
            {
                return NotFound("Brand Not Found");
            }

            var models = await _db.Models
                                  .Where(model => model.Brand!.Name!.ToUpper() == brand.Name!.ToUpper())
                                  .Select(model => new
                                  {
                                      model.Id,
                                      model.Name,
                                      model.ManufactureYear,
                                      Category = new
                                      {
                                          model.Category!.Id,
                                          model.Category.Name
                                      }
                                  })
                                  .ToListAsync();

            if (models is null)
            {
                return Ok(new List<CarModel>());
            }

            return Ok(models);
        }

        [HttpGet("get-car-model-by-category-and-brand")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> GetCarModelsByCategoryAndBrand(string categoryName,string bradnName)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name!.ToUpper() == categoryName.ToUpper());
            var brand = await _db.Brands.FirstOrDefaultAsync(c => c.Name!.ToUpper() == bradnName.ToUpper());

            if (brand is null || category is null)
            {
                return NotFound("Category Or Brand Not Found");
            }

            var models = await _db.Models
                                  .Where(model => model.Brand!.Name!.ToUpper() == brand.Name!.ToUpper() 
                                                  && model.Category!.Name!.ToUpper() == categoryName.ToUpper())
                                  .Select(model => new
                                  {
                                      model.Id,
                                      model.Name,
                                      model.ManufactureYear
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
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> AddCarCategory(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryExists = await _db.Categories.FirstOrDefaultAsync(category => category.Name!.ToUpper() == name.ToUpper());

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

            var brandExists = await _db.Brands.FirstOrDefaultAsync(brand => brand.Name!.ToUpper() == name.ToUpper());

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

            var modelExists = await _db.Models
                                            .FirstOrDefaultAsync(model => model.Name!.ToUpper() == name.ToUpper());
            var brand = await _db.Brands
                                        .FirstOrDefaultAsync(brand => brand.Name!.ToUpper() == brandName.ToUpper());
            var category = await _db.Categories
                                    .FirstOrDefaultAsync(category => category.Name!.ToUpper() == categoryName.ToUpper());

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
