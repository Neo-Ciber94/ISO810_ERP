
namespace ISO810_ERP.Models;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string ShortCode { get; set; } = default!;
    public string Symbol { get; set; } = default!;
}