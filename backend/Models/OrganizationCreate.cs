
namespace ISO810_ERP.Models;

public class OrganizationCreate
{
    public int AccountId { get; set; }
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;
}