using MongoDB.Driver;
using Monolithic.Core.Domain.Customer.Contract.Repository;
using Monolithic.Core.Domain.Customer.Model;
using Monolithic.Core.Infrastructure.Context;

namespace Monolithic.Core.Infrastructure.Repositories;
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customerCollection;

    public CustomerRepository(MongoDbContext context) 
        : base(context.Database)
    {
        _customerCollection = context.Database.GetCollection<Customer>(nameof(Customer));
    }
    public async Task<Customer> GetByEmail(string documentEmail)
    {
        var filter = Builders<Customer>.Filter.Where(p => p.Email == documentEmail);
        var customer =  await _customerCollection.FindAsync(filter);
        return await customer.FirstOrDefaultAsync();
    }
}