
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISO810_ERP.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}