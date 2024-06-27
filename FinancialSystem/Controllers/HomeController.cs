using FinancialSystem.Data;
using FinancialSystem.Models;
using FinancialSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinancialSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            List<Invoice> invoices = dbContext.Invoices.ToList();
            return View(invoices);
        }

        // GET: /FilterInvoices
        [HttpGet]
        public IActionResult FilterInvoices(int? issuanceMonth, int? billingMonth, int? paymentMonth, string status)
        {
            IQueryable<Invoice> query = dbContext.Invoices.AsQueryable();

            if (issuanceMonth.HasValue)
            {
                query = query.Where(i => i.IssuanceDate.Month == issuanceMonth);
            }

            if (billingMonth.HasValue)
            {
                query = query.Where(i => i.BillingDate.HasValue && i.BillingDate.Value.Month == billingMonth);
            }

            if (paymentMonth.HasValue)
            {
                query = query.Where(i => i.PaymentDate.HasValue && i.PaymentDate.Value.Month == paymentMonth);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(i => ((int)i.Status) == Int32.Parse(status));
            }

            List<Invoice> filteredInvoices = query.ToList();
            return PartialView("_InvoiceListPartial", filteredInvoices);
        }
    }
}
