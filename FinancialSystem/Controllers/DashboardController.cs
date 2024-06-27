using FinancialSystem.Data;
using FinancialSystem.Models;
using FinancialSystem.Models.Entities;
using FinancialSystem.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DashboardController(ApplicationDbContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData(string period, int? year, int? month, int? quarter)
        {
            var model = new DashboardViewModel();
            IQueryable<Invoice> invoices = _context.Invoices.AsQueryable();

            if (year.HasValue)
            {
                invoices = invoices.Where(i => i.IssuanceDate.Year == year.Value);
            }

            if (month.HasValue)
            {
                invoices = invoices.Where(i => i.IssuanceDate.Month == month.Value);
            }

            if (quarter.HasValue)
            {
                var startMonth = (quarter.Value - 1) * 3 + 1;
                invoices = invoices.Where(i => i.IssuanceDate.Month >= startMonth && i.IssuanceDate.Month <= startMonth + 2);
            }

            var invoiceList = invoices.ToList();

            model.Values = new List<decimal> {
                invoiceList.Sum(i => i.Amount),
                invoiceList.Where(i => i.BillingDate == null).Sum(i => i.Amount),
                invoiceList.Where(i => i.PaymentDate < DateTime.Today && i.Status == InvoiceStatus.PaymentOverdue).Sum(i => i.Amount),
                invoiceList.Where(i => i.PaymentDate >= DateTime.Today && i.Status == InvoiceStatus.PaymentOverdue).Sum(i => i.Amount),
                invoiceList.Where(i => i.Status == InvoiceStatus.PaymentCompleted).Sum(i => i.Amount)
            };
            model.Labels = new List<string> { "Notas Emitidas", "Notas Não Cobradas", "Notas Vencidas", "Notas a Vencer", "Notas Pagas" };

            switch (period)
            {
                case "month":
                    model.OverdueValues = GetMonthlyData(invoiceList, i => i.PaymentDate.HasValue && i.PaymentDate < DateTime.Today && i.Status == InvoiceStatus.PaymentOverdue);
                    model.OverdueLabels = GetMonthlyLabels();
                    model.RevenueValues = GetMonthlyData(invoiceList, i => i.Status == InvoiceStatus.PaymentCompleted);
                    model.RevenueLabels = GetMonthlyLabels();
                    break;
                case "quarter":
                    model.OverdueValues = GetQuarterlyData(invoiceList, i => i.PaymentDate.HasValue && i.PaymentDate < DateTime.Today && i.Status == InvoiceStatus.PaymentOverdue);
                    model.OverdueLabels = GetQuarterlyLabels();
                    model.RevenueValues = GetQuarterlyData(invoiceList, i => i.Status == InvoiceStatus.PaymentCompleted);
                    model.RevenueLabels = GetQuarterlyLabels();
                    break;
                case "year":
                    model.OverdueValues = GetYearlyData(invoiceList, i => i.PaymentDate.HasValue && i.PaymentDate < DateTime.Today && i.Status == InvoiceStatus.PaymentOverdue);
                    model.OverdueLabels = GetYearlyLabels();
                    model.RevenueValues = GetYearlyData(invoiceList, i => i.Status == InvoiceStatus.PaymentCompleted);
                    model.RevenueLabels = GetYearlyLabels();
                    break;
                default:
                    return BadRequest("Período inválido.");
            }

            var data = new
            {
                Values = model.Values,
                Labels = model.Labels,
                OverdueValues = model.OverdueValues,
                OverdueLabels = model.OverdueLabels,
                RevenueValues = model.RevenueValues,
                RevenueLabels = model.RevenueLabels
            };

            return Json(data);
        }

        private List<decimal> GetMonthlyData(List<Invoice> invoices, Func<Invoice, bool> predicate)
        {
            return invoices.Where(predicate).GroupBy(i => i.IssuanceDate.Month).OrderBy(g => g.Key).Select(g => g.Sum(i => i.Amount)).ToList();
        }

        private List<string> GetMonthlyLabels()
        {
            return new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
        }

        private List<decimal> GetQuarterlyData(List<Invoice> invoices, Func<Invoice, bool> predicate)
        {
            return invoices.Where(predicate).GroupBy(i => (i.IssuanceDate.Month - 1) / 3).OrderBy(g => g.Key).Select(g => g.Sum(i => i.Amount)).ToList();
        }

        private List<string> GetQuarterlyLabels()
        {
            return new List<string> { "Q1", "Q2", "Q3", "Q4" };
        }

        private List<decimal> GetYearlyData(List<Invoice> invoices, Func<Invoice, bool> predicate)
        {
            return invoices.Where(predicate).GroupBy(i => i.IssuanceDate.Year).OrderBy(g => g.Key).Select(g => g.Sum(i => i.Amount)).ToList();
        }

        private List<string> GetYearlyLabels()
        {
            return Enumerable.Range(1, 12).Select(m => new DateTime(2020, m, 1).ToString("MMM")).ToList();
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            var statuses = Enum.GetValues(typeof(InvoiceStatus)).Cast<InvoiceStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }).ToList();
            return Json(statuses);
        }

        [HttpGet]
        public IActionResult GetYears()
        {
            var years = _context.Invoices.Select(i => i.IssuanceDate.Year).Distinct().OrderBy(y => y).ToList();
            return Json(years);
        }
    }
}
