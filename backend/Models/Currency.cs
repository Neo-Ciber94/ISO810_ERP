
using System.ComponentModel.DataAnnotations;
using ISO810_ERP.Config;

namespace ISO810_ERP.Models;

public class Currency
{
    public int Id { get; set; }

    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;

    [MinLength(Constants.MinSymbolLength)]
    public string ShortCode { get; set; } = default!;

    [MinLength(Constants.MinSymbolLength)]
    public string Symbol { get; set; } = default!;
}