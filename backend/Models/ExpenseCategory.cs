using System.ComponentModel.DataAnnotations;
using ISO810_ERP.Config;

namespace ISO810_ERP.Models;

public class ExpenseCategory
{
    public int Id { get; set; }

    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;
}
