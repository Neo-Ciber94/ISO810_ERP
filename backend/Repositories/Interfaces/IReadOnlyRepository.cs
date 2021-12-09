using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISO810_ERP.Repositories.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    ValueTask<TEntity?> GetById(int id);
}