
namespace ISO810_ERP.Controllers;


public class PaginationQuery
{
    public const string SortAscending = "asc";
    public const string SortDescending = "desc";
    
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? Sort { get; set; }
    public string? SortDir { get; set; }
}