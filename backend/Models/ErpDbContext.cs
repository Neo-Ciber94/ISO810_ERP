using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

public class ErpDbContext : DbContext
{
    public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<Expense> Expenses { get; set; } = default!;
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
}