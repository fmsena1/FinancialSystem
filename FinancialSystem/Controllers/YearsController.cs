using FinancialSystem.Data;
using FinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinancialSystem.Controllers
{
    [Route("Year")]
    public class YearController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YearController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetYears")]
        public IActionResult GetYears()
        {
            var years = _context.Invoices.Select(i => i.IssuanceDate.Year).Distinct().OrderBy(y => y).Select(y => new YearViewModel
            {
                Year = y
            }).ToList();
            return Json(years);
        }
    }
}
