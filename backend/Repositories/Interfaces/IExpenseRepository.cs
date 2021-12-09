
using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;

namespace ISO810_ERP.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        public IQueryable<ExpenseCategory> GetCategories();
        
        public ValueTask<ExpenseCategory?> GetCategoryById(int id);

        public IQueryable<ExpenseDto>? GetAll(int accountId, int organizationId);

        public ValueTask<ExpenseDto?> GetById(int accountId, int organizationId, int expenseId);

        public Task<ExpenseDto> Create(int accountId, ExpenseCreate expense);

        public Task<ExpenseDto> Update(int accountId, ExpenseUpdate expense);

        public Task<ExpenseDto> Delete(int accountId, int expenseId);
    }
}