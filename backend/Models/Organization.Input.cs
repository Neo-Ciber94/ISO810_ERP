
namespace ISO810_ERP.Models;

public class OrganizationCreate
{
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;
}

public class OrganizationUpdate
{
    public string? Name { get; set; }
    public string? Alias { get; set; }
}