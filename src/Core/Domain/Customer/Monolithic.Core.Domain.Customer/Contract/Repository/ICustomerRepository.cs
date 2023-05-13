using Monolithic.Core.SharedKernel.Contract;

namespace Monolithic.Core.Domain.Customer.Contract.Repository;
public interface ICustomerRepository : IRepository<Model.Customer>
{
    Task<Model.Customer> GetByEmail(string documentEmail);
}