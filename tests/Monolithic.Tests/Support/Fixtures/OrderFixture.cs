using Bogus;
using Monolithic.Core.Domain.Order.Model;

namespace Monolithic.Tests.Support.Fixtures;
public class OrderFixture : IDisposable
{
    public OrderFixture() { }

    public IEnumerable<Order> GetValidOrders()
    {
        var orders = new List<Order>();
        orders.AddRange(GenerateValidOrder(10, true));
        orders.AddRange(GenerateValidOrder(10, false));
        return orders;
    }

    public Order? GetValidOrder() 
        => GenerateValidOrder(1, true).FirstOrDefault();

    public Order? GetInvalidOrder() 
        => GenerateInvalidOrder(1, true).FirstOrDefault();
    
    private IEnumerable<Order> GenerateValidOrder(int quantity, bool active)
    {
        var order = new Faker<Order>("pt_BR")
           .CustomInstantiator(f => new Order(
                customerId: Guid.NewGuid(),
                active: active,
                items: new List<OrderItem>() 
           ));
        return order.Generate(quantity);
    }

    private IEnumerable<Order> GenerateInvalidOrder(int quantity, bool active)
    {
        var order = new Faker<Order>("pt_BR")
           .CustomInstantiator(f => new Order(
                customerId: Guid.Empty,
                active: active,
                items: new List<OrderItem>()
           ));
        return order.Generate(quantity);
    }

    public void Dispose() { }
}