using System;

namespace ISO810_ERP.Dtos;

public class ExpenseDto
{
    public int Id { get; set; }
    public int OrganizationId { get; set; } = default!;
    public int ServiceId { get; set; } = default!;
    public int CurrencyId { get; set; } = default!;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}