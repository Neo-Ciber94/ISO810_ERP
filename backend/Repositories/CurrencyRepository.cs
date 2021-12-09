using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;

namespace ISO810_ERP.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ErpDbContext context;
    
    public CurrencyRepository(ErpDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Currency> GetAll()
    {
        return context.Currencies;
    }

    public ValueTask<Currency?> GetById(int id)
    {
        return context.Currencies.FindAsync(id);
    }
}