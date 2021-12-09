
using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;

namespace ISO810_ERP.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ErpDbContext context;

    public ServiceRepository(ErpDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Service> GetAll()
    {
        return context.Services;
    }

    public ValueTask<Service?> GetById(int id)
    {
        return context.Services.FindAsync(id);
    }
}