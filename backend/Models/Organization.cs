
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

public class Organization
{
    public int Id { get; set; }
    public Account Account { get; set; } = default!;
    public int AccountId { get; set; }
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}