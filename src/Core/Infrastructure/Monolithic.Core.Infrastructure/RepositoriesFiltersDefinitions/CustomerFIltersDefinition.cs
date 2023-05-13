using MongoDB.Driver;
using Monolithic.Core.Domain.Customer.Model;

namespace Monolithic.Core.Infrastructure.RepositoriesFiltersDefinitions;
public class CustomerFIltersDefinition
{
    public static FilterDefinition<Customer> CreateFIlters(
        string name,
        string email,
        string cellPhone,
        bool active)
    {
        var builder = Builders<Customer>.Filter;
        var filter = builder.Empty;

        if(!string.IsNullOrEmpty(name))
            filter &= builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
        
        if(!string.IsNullOrEmpty(email))
            filter &= builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(email, "i"));

        if(!string.IsNullOrEmpty(cellPhone))
            filter &= builder.Eq(x => x.CellPhone, cellPhone);

        if(!string.IsNullOrEmpty(cellPhone))
            filter &= builder.Eq(x => x.Active, active);

        return filter;
    }
}