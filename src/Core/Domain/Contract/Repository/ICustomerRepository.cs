namespace Monolithic.Core.Domain.Contract.Repository;
public interface ICustomerRepository : IRepository<Customer>
{
    Task<TEntity> GetByEmail(string documentEmail);
}