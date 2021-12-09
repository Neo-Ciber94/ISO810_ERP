
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

    public PaginationResult<TResult> Map<TResult>(Func<IEnumerable<T>, IEnumerable<TResult>> mapper)
    {
        return new PaginationResult<TResult>
        {
            Page = Page,
            PageSize = PageSize,
            TotalPages = TotalPages,
            TotalCount = TotalCount,
            HasPreviousPage = HasPreviousPage,
            HasNextPage = HasNextPage,
            Items = mapper(Items)
        };
    }
}