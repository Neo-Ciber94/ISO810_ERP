
using System;

namespace ISO810_ERP.Dtos;

public class OrganizationDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}