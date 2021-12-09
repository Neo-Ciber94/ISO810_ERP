
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
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseRepository expenseRepository;

    public ExpenseController(IExpenseRepository expenseRepository)
    {
        this.expenseRepository = expenseRepository;
    }

    [HttpGet("category")]
    [ProducesResponseType(typeof(IEnumerable<ExpenseCategory>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetExpenseCategories()
    {
        var result = await expenseRepository.GetCategories().ToListAsync();
        return Ok(result);
    }

    [HttpGet("category/{id}")]
    [ProducesResponseType(typeof(IEnumerable<ExpenseCategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetCagetory(int id)
    {
        var result = await expenseRepository.GetCategoryById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{organizationId}")]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses(int organizationId)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = expenseRepository.GetAll(currentAccount.Id, organizationId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(await result.ToListAsync());
    }

    [HttpGet("{organizationId}/{expenseId}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Expense>> GetExpense(int organizationId, int expenseId)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await expenseRepository.GetById(currentAccount.Id, organizationId, expenseId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Expense>> Create(ExpenseCreate expense)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await expenseRepository.Create(currentAccount.Id, expense);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{organizationId}/{expenseId}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Expense>> Update(int organizationId, int expenseId, [FromBody] ExpenseUpdate expense)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        expense.OrganizationId = organizationId;

        var result = await expenseRepository.Update(currentAccount.Id, expenseId, expense);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{expenseId}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Expense>> Delete(int expenseId)
    {
        var currentAccount = HttpContext.GetCookieUserAccount();

        if (currentAccount == null)
        {
            return NotFound();
        }

        var result = await expenseRepository.Delete(currentAccount.Id, expenseId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
