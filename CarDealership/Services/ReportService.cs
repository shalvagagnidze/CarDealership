using AutoMapper;
using CarDealership.Data;
using CarDealership.Entities;
using CarDealership.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Services
{
    public class ReportService : IReportService
    {
        private readonly CarDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public ReportService(CarDbContext db, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateReport(ReportModel reportModel, string userName,int carId)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var car = await _db.Models.FirstOrDefaultAsync(model => model.Id == carId);

            if(user is null)
            {
                return new NotFoundObjectResult("User Not Found!");
            }

            var report = new Report
            {
                Title = reportModel.Title,
                Price = reportModel.Price,
                Description = reportModel.Description,
                CarModel = car,
                User = (User?)user,
            };

            await _db.Reports.AddAsync(report);
            var result = await _db.SaveChangesAsync();

            if(result > 0)
            {
                return new OkObjectResult("Report Has Created Successfully!");
            }

            return new BadRequestObjectResult("Error occured");
        }
    }
}
