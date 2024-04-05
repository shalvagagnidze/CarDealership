using CarDealership.Data;
using CarDealership.Entities;
using CarDealership.Models;
using CarDealership.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarDealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly CarDbContext _db;
        private readonly IReportService _reportService;

        public SalesController(CarDbContext db, IReportService reportService)
        {
            _db = db;
            _reportService = reportService;
        }

        [HttpGet("get-reports")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetReports()
        {

            var reports = await _db.Reports
                                   .Select(report => new
                                   {
                                       report.Title,
                                       report.Price,
                                       report.Description,
                                       Model = new
                                       {
                                           report.CarModel!.Id,
                                           report.CarModel.Name
                                       },
                                       Category = new
                                       {
                                           report.CarModel.Category!.Id,
                                           report.CarModel.Category.Name
                                       },
                                       Brand = new
                                       {
                                           report.CarModel.Brand!.Id,
                                           report.CarModel.Brand.Name
                                       }
                                   }).ToListAsync();

            return Ok(reports);
        }

        [HttpGet("get-reports-by-user-id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetReportsByUserId(string id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return BadRequest("User Not Found!");
            }

            var reports = await _db.Reports
                                   .Where(report => report.User == user)
                                   .Select(report => new
                                   {
                                       report.Title,
                                       report.Price,
                                       report.Description,
                                       Model = new
                                       {
                                           report.CarModel!.Id,
                                           report.CarModel.Name
                                       },
                                       Category = new
                                       {
                                           report.CarModel.Category!.Id,
                                           report.CarModel.Category.Name
                                       },
                                       Brand = new
                                       {
                                           report.CarModel.Brand!.Id,
                                           report.CarModel.Brand.Name
                                       }
                                   }).ToListAsync();

            return Ok(reports);
        }

        [HttpGet("get-reposts-by-price-range")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetReportsByPriceRange(decimal lowestPrice, decimal highestPrice)
        {
            if (lowestPrice < 0)
            {
                return BadRequest("The lowest price shouldn't be a negative integer");
            }

            if (highestPrice == 0 || highestPrice < 0)
            {
                return BadRequest("The highest price must be a positive integer");
            }

            if (highestPrice < lowestPrice)
            {
                return BadRequest("The highest price must be equal ro or greater than the lowest price");
            }

            var reports = await _db.Reports.Where(report => report.Price >= lowestPrice && report.Price <= highestPrice)
                                           .Select(report => new
                                           {
                                               report.Title,
                                               report.Price,
                                               report.Description,
                                               Model = new
                                               {
                                                   report.CarModel!.Id,
                                                   report.CarModel.Name
                                               },
                                               Category = new
                                               {
                                                   report.CarModel.Category!.Id,
                                                   report.CarModel.Category.Name
                                               },
                                               Brand = new
                                               {
                                                   report.CarModel.Brand!.Id,
                                                   report.CarModel.Brand.Name
                                               }
                                           }).ToListAsync();

            if (reports is not null)
            {
                return Ok(reports);
            }

            return Ok(new List<ReportModel>());

        }

        [HttpPost("add-report")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> CreateReport(ReportModel report, int carModelId)
        {
            var user = User.FindFirst(ClaimTypes.Name)?.Value;

            return await _reportService.CreateReport(report, user!, carModelId);
        }
    }
}
