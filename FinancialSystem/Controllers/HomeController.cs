using FinancialSystem.Data;
using FinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using FinancialSystem.Resources;
using FinancialSystem.Models.Entities;

namespace FinancialSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public HomeController(ApplicationDbContext dbContext, IStringLocalizer<SharedResource> localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }

        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            List<InvoiceViewModel> invoices = _dbContext.Invoices
                .Select(i => new InvoiceViewModel
                {
                    Id = i.Id,
                    PayerName = i.PayerName,
                    InvoiceNumber = i.InvoiceNumber,
                    IssuanceDate = i.IssuanceDate,
                    BillingDate = i.BillingDate,
                    PaymentDate = i.PaymentDate,
                    Amount = i.Amount,
                    InvoiceDocument = i.InvoiceDocument,
                    BankSlipDocument = i.BankSlipDocument,
                    Status = i.Status.ToString(),
                })
                .ToList();

            return View(invoices);
        }

        // GET: /FilterInvoices
        [HttpGet]
        public IActionResult FilterInvoices(int? issuanceMonth, int? billingMonth, int? paymentMonth, string status)
        {
            IQueryable<Invoice> query = _dbContext.Invoices.AsQueryable();

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
                query = query.Where(i => ((int)i.Status).ToString() == status);
            }

            List<InvoiceViewModel> filteredInvoices = query
                .Select(i => new InvoiceViewModel
                {
                    Id = i.Id,
                    PayerName = i.PayerName,
                    InvoiceNumber = i.InvoiceNumber,
                    IssuanceDate = i.IssuanceDate,
                    BillingDate = i.BillingDate,
                    PaymentDate = i.PaymentDate,
                    Amount = i.Amount,
                    InvoiceDocument = i.InvoiceDocument,
                    BankSlipDocument = i.BankSlipDocument,
                    Status = i.Status.ToString(),
                })
                .ToList();

            return PartialView("_InvoiceListPartial", filteredInvoices);
        }
    }
}
