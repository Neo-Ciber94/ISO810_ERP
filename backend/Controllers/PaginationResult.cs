
using System;
using System.Collections.Generic;

namespace ISO810_ERP.Controllers;

public class PaginationResult<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<T> Items { get; set; } = Array.Empty<T>();
}