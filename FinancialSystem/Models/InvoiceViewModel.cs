﻿namespace FinancialSystem.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public string? PayerName { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime IssuanceDate { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? InvoiceDocument { get; set; }
        public string? BankSlipDocument { get; set; }
        public string Status { get; set; }
    }
}