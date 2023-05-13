using Monolithic.Core.SharedKernel.DomainObjects;
using Monolithic.Core.SharedKernel.Enums;

namespace Monolithic.Core.SharedKernel.Contract;
public interface IRepository<TEntity> where TEntity : AbstractEntity
{
    Task<IEnumerable<TEntity>> FindAll(string sortBy, ESortDirection sortDirection, int page, int offset);
    Task<TEntity> Find(string id);
    Task<TEntity>  Insert(TEntity entity);
    Task Update(TEntity entity, string id);
    Task<long> CountDocuments();
}