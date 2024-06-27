using FinancialSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinancialSystem.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
    }
}


