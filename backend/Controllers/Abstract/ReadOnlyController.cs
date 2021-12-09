
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Extensions;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Controllers.Abstract;

public abstract class ReadOnlyController<TEntity> : ControllerBase where TEntity : class
{
    private readonly IReadOnlyRepository<TEntity> repository;

    public ReadOnlyController(IReadOnlyRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        var result = await repository.GetAll().ToListAsync();
        return Ok(result);
    }

    [HttpGet("query")]
    public virtual async Task<ActionResult<PaginationResult<TEntity>>> Query([FromQuery] PaginationQuery query)
    {
        int page = Math.Max(1, query.Page.GetValueOrDefault(1));
        int pageSize = Math.Max(1, query.PageSize.GetValueOrDefault(10));

        IQueryable<TEntity> queryable = repository.GetAll();

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

        return Ok(pageResult);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> GetById(int id)
    {
        var result = await repository.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}