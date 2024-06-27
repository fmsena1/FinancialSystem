using System;
using System.Collections.Generic;

namespace FinancialSystem.Models
{
    public class DashboardViewModel
    {
        public List<decimal> Values { get; set; }
        public List<string> Labels { get; set; }
        public List<decimal> OverdueValues { get; set; }
        public List<string> OverdueLabels { get; set; }
        public List<decimal> RevenueValues { get; set; }
        public List<string> RevenueLabels { get; set; }
    }
}
