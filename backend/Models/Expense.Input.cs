using System;
using System.ComponentModel.DataAnnotations;

namespace ISO810_ERP.Models;

public class ExpenseCreate
{
    public int OrganizationId { get; set; } = default!;
    public int ServiceId { get; set; } = default!;
    public int CurrencyId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "The amount must be greater than 0")]
    public decimal Amount { get; set; }
}

public class ExpenseUpdate
{
    public int? OrganizationId { get; set; } = default!;
    public int? ServiceId { get; set; } = default!;
    public int? CurrencyId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "The amount must be greater than 0")]
    public decimal? Amount { get; set; }
}