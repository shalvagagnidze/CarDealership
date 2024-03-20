using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Services
{
    public interface IReportService
    {
        Task<IActionResult> CreateReport(ReportModel report,string userId,int carId);
    }
}
