
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISO810_ERP.Config;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

[Index(nameof(Email), IsUnique = true)]
public class Account
{
    public int Id { get; set; }

    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;

    [MinLength(Constants.MinNameLength)]
    public string Email { get; set; } = default!;
    
    public string PasswordHash { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}