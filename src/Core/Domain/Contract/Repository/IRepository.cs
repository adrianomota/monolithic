namespace Monolithic.Core.Domain.Contract.Repository;
public interface IRepository<TEntity> where TEntity : AbstractEntity
{
    Task<IEnumeravle<TEntity>> FindAll(int page, int offset);
    Task<TEntity> Find(string id);
    Task Insert(TEnitity entity);
    Task Update(TEntity entity, string id);
}