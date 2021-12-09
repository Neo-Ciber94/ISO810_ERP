
using System.ComponentModel.DataAnnotations;
using ISO810_ERP.Config;

namespace ISO810_ERP.Models;

public class AccountSignup
{
    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;
    
    [MinLength(Constants.MinNameLength)]
    public string Email { get; set; } = default!;

    [MinLength(Constants.MinPasswordLength)]
    public string Password { get; set; } = default!;
}