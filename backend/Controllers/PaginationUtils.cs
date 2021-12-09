
using System;
using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Extensions;
using ISO810_ERP.Utils;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Controllers;

public static class PaginationUtils
{
    public static async Task<PaginationResult<TEntity>> Paginate<TEntity>(IQueryable<TEntity> queryable, PaginationQuery query)
    {
        int page = Math.Max(1, query.Page.GetValueOrDefault(1));
        int pageSize = Math.Max(1, query.PageSize.GetValueOrDefault(10));

        if (query.Sort != null)
        {
            var sortDir = query.SortDir switch
            {
                PaginationQuery.SortAscending => query.SortDir,
                PaginationQuery.SortDescending => query.SortDir,
                _ => PaginationQuery.SortAscending
            };

            var property = ReflectionUtils.GetPropertyIgnoreCase(typeof(TEntity), query.Sort!);

            if (property != null)
            {
                if (sortDir == PaginationQuery.SortAscending)
                {
                    queryable = queryable.OrderByProperty(property.Name);
                }
                else
                {
                    queryable = queryable.OrderByDescendingProperty(property.Name);
                }
            }
        }

        var items = await queryable
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling((double)queryable.Count() / pageSize);
        var totalCount = await queryable.CountAsync();

        var pageResult = new PaginationResult<TEntity>
        {
            Items = items,
            Page = page,
            PageSize = Math.Min(items.Count, pageSize),
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPage = page < totalPages,
            HasPreviousPage = page > 1
        };

        return pageResult;
    }
}