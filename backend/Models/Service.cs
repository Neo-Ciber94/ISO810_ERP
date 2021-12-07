using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISO810_ERP.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string HomePage { get; set; } = default!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}