using Bogus;
using Monolithic.Core.Domain.Order.Model;
public class OrderItemFixture : IDisposable
{
    public OrderItemFixture()
    {
        
    }

    public IList<OrderItem> GetValidOrderItems()
    {
        var orderItems = new List<OrderItem>();
        orderItems.AddRange(GenerateValidOrderItem(10, true));
        orderItems.AddRange(GenerateValidOrderItem(10, false));
        return orderItems;
    }
    
    public OrderItem? GetinvalidOrderItem()
        => GenerateInvalidOrderItem(1, true).FirstOrDefault();

    public OrderItem? GetValidOrderItem()
        => GenerateValidOrderItem(1, true).FirstOrDefault();

    private IEnumerable<OrderItem> GenerateValidOrderItem(int quantity, bool active)
    {
        var orderItem = new Faker<OrderItem>("pt_BR")
            .CustomInstantiator(f => new OrderItem(
                name: f.Commerce.ProductName(),
                quantity: 10,
                price: decimal.Parse(f.Commerce.Price()),
                active: active
            ));

        return orderItem.Generate(quantity);
    }
    private IEnumerable<OrderItem> GenerateInvalidOrderItem(int quantity, bool active)
    {
        var orderItem = new Faker<OrderItem>("pt_BR")
            .CustomInstantiator(f => new OrderItem(
                name: "",
                quantity: 0,
                price: 0,
                active: active
            ));

        return orderItem.Generate(quantity);
    }
 
    public void Dispose() { }
}