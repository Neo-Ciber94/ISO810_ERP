
using System.Collections.Generic;
using System.Threading.Tasks;
using ISO810_ERP.Dtos;
using ISO810_ERP.Extensions;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationRepository organizationRepository;

    public OrganizationController(IOrganizationRepository organizationRepository)
    {
        this.organizationRepository = organizationRepository;
    }

    /// <summary>
    /// Get all the organizations for the current account.
    /// </summary>
    /// <returns>The organization created by the current account.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrganizationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAll()
    {
        var currentAccount = HttpContext.GetCookieUserAccount();
        
        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await organizationRepository.GetAll(currentAccount.Id).ToListAsync();
        return Ok(result);
    }

    /// <summary>
    /// Gets the organization by id.
    /// </summary>
    /// <param name="id">The id of the organization.</param>
    /// <returns>An organization with the given id for the current user.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> GetById(int id)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await organizationRepository.GetById(currentAccount.Id, id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates an organization for the current account.
    /// </summary>
    /// <param name="organizationDto">The info of the organization to create.</param>
    /// <returns>The newly create organization.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> Create(OrganizationCreate organizationDto)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await organizationRepository.Create(currentAccount.Id, organizationDto);

        if (result == null)
        {
            return NotFound();
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    /// <summary>
    /// Updates an organization for the current account.
    /// </summary>
    /// <param name="id">The id of the organization to update.</param>
    /// <param name="organizationDto">The info to update.</param>
    /// <returns>The newly updated organization.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> Update(int id, OrganizationUpdate organizationDto)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await organizationRepository.Update(currentAccount.Id, id, organizationDto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an organization for the current account.
    /// </summary>
    /// <param name="id">The id of the organization.</param>
    /// <returns>The deleted organization</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> Delete(int id)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await organizationRepository.Delete(currentAccount.Id, id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}