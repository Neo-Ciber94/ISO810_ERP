using System.Threading.Tasks;

namespace ISO810_ERP.Repositories.Interfaces;

public interface IRepository<TEntity, TAdd, TUpdate> : IReadOnlyRepository<TEntity> where TEntity : class
{
    Task<TEntity> Add(TAdd entity);
    Task<TEntity> Update(int id, TUpdate entity);
    Task<TEntity> Remove(int id);
    Task<int> SaveChanges();
}

public interface IRepository<TEntity> : IRepository<TEntity, TEntity, TEntity> where TEntity : class
{
    new Task<TEntity> Add(TEntity entity);
    new Task<TEntity> Update(int id, TEntity entity);
}