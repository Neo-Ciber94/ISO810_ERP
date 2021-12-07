
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

[Index(nameof(Account), IsUnique = true)]
public class Organization
{
    public int Id { get; set; }
    public long Account { get; set; }
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}