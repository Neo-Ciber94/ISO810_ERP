using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISO810_ERP.Models;

public class Expense
{
    public int Id { get; set; }
    public int OrganizationId { get; set; } = default!;
    public Organization Organization { get; set; } = default!;
    public int ServiceId { get; set; } = default!;
    public Service Service { get; set; } = default!;
    public int CurrencyId { get; set; } = default!;
    public Currency Currency { get; set; } = default!;
    public decimal Amount { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}