
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ErpDbContext context;
    private readonly IMapper mapper;

    public ExpenseRepository(ErpDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public IQueryable<ExpenseCategory> GetCategories()
    {
        return context.ExpenseCategories;
    }

    public ValueTask<ExpenseCategory?> GetCategoryById(int id)
    {
        return context.ExpenseCategories.FindAsync(id);
    }

    public IQueryable<ExpenseDto>? GetAll(int account, int organizationId)
    {
        var organization = context.Organizations.FirstOrDefault(o => o.Id == organizationId && o.AccountId == account);

        if (organization == null)
        {
            return null;
        }

        return context.Expenses
            .Where(e => e.OrganizationId == organizationId)
            .Select(e => mapper.Map<ExpenseDto>(e));
    }

    public async ValueTask<ExpenseDto?> GetById(int account, int organizationId, int expenseId)
    {
        return await context.Expenses
            .Include(e => e.Organization)
            .Where(e => e.Organization.AccountId == account && e.OrganizationId == organizationId && e.Id == expenseId)
            .Select(e => mapper.Map<ExpenseDto>(e))
            .SingleOrDefaultAsync();
    }

    public Task<ExpenseDto> Create(int accountId, ExpenseCreate expense)
    {
        throw new NotImplementedException();
    }

    public Task<ExpenseDto> Update(int accountId, ExpenseUpdate expense)
    {
        throw new NotImplementedException();
    }

    public Task<ExpenseDto> Delete(int accountId, int expenseId)
    {
        throw new NotImplementedException();
    }
}