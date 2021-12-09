using System;
using System.ComponentModel.DataAnnotations;
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

    [Column(TypeName = "decimal(18,2)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "The amount must be greater than 0")]
    public decimal Amount { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}