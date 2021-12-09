using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISO810_ERP.Config;

namespace ISO810_ERP.Models;

public class Service
{
    public int Id { get; set; }

    [MinLength(Constants.MinNameLength)]
    public string Name { get; set; } = default!;

     [MinLength(Constants.MinNameLength)]
    public string HomePage { get; set; } = default!;
}