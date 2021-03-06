
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Extensions;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Controllers.Abstract;

public abstract class ReadOnlyController<TEntity> : ReadOnlyController<TEntity, TEntity> where TEntity : class
{
    protected ReadOnlyController(IReadOnlyRepository<TEntity> repository, IMapper mapper) : base(repository, mapper) { }
}

[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public abstract class ReadOnlyController<TEntity, TDto> : ControllerBase where TEntity : class
{
    private readonly IReadOnlyRepository<TEntity> repository;
    private readonly IMapper mapper;

    protected ReadOnlyController(IReadOnlyRepository<TEntity> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all the entities.
    /// </summary>
    /// <returns>All the entities stored.</returns>
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        var result = await repository.GetAll().ToListAsync();

        if (typeof(TEntity) != typeof(TDto))
        {
            var mappedResult = mapper.Map<IEnumerable<TDto>>(result);
            return Ok(mappedResult);
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets a paginated result of the entities.
    /// </summary>
    /// <param name="query">The pagination query.</param>
    /// <returns>A paginated result.</returns>
    [HttpGet("query")]
    public virtual async Task<ActionResult<PaginationResult<TDto>>> Query([FromQuery] PaginationQuery query)
    {
        IQueryable<TEntity> queryable = repository.GetAll();
        var result = await PaginationUtils.Paginate(queryable, query);

        if (typeof(TEntity) != typeof(TDto))
        {
            var mappedResult = result.Map((items) => mapper.Map<IEnumerable<TDto>>(items));
            return Ok(mappedResult);
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets the entity with the given id.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <returns>An entity with the given id if found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<TDto>> GetById(int id)
    {
        var result = await repository.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        if (typeof(TEntity) != typeof(TDto))
        {
            var mappedResult = mapper.Map<TDto>(result);
            return Ok(mappedResult);
        }

        return Ok(result);
    }
}