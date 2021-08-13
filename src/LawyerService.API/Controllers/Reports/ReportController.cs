using LawyerService.BL.Interfaces.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportController : Controller
    {
        private readonly IReportManager _reportManager;

        public ReportController(IReportManager reportManager)
        {
            _reportManager = reportManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ReportAdmin(string report, int year)
        {
            var webReport = await _reportManager.CreateReportAdminAllAsync(report, year);
            ViewBag.WebReport = webReport;
            return View();
        }
    }
}
