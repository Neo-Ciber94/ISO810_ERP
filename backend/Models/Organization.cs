
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISO810_ERP.Config;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

public class Organization
{
    public int Id { get; set; }
    public Account Account { get; set; } = default!;
    public int AccountId { get; set; }

    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;

    [MinLength(Constants.MinNameLength)]
    public string Alias { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}