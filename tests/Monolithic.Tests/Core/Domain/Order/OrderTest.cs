using Monolithic.Tests.Support.Fixtures;
using OrderItemDomain =  Monolithic.Core.Domain.Order.Model;
namespace Monolithic.Tests.Core.Domain.Order;
[Collection(nameof(OrderCollection))]
public class OrderTest
{
    private readonly OrderFixture _orderFixture;
 
    public OrderTest(OrderFixture orderFixture)
    {
        _orderFixture = orderFixture;
    }

    [Fact]
    public void When_i_have_valid_order_params_return_success()
    {
        var order = _orderFixture.GetValidOrder();
        order?.AddItem(new OrderItemDomain.OrderItem(name: "Product 1", quantity: 2, price: 10.0m, true));
        order?.Validate();
        Assert.True(order?.IsValid());
    }

    [Fact]
    public void When_i_have_invalid_customer_id_return_error()
    {
        var order = _orderFixture.GetValidOrder();
        order?.AddItem(new OrderItemDomain.OrderItem(name: "Product 1", quantity: 2, price: 10.0m, true));
        order?.ChangeCustomer(Guid.Empty);
        order?.Validate();
        Assert.False(order?.IsValid());
    }

    [Fact]
    public void When_i_have_two_items_wirh_valid_price_return_success()
    {
        var order = _orderFixture.GetValidOrder();
        order?.AddItems(
            new List<OrderItemDomain.OrderItem>()
            {
                new OrderItemDomain.OrderItem(name: "Product 1", quantity: 2, price: 100.20m, true),
                new OrderItemDomain.OrderItem(name: "Product 2", quantity: 3, price: 100.99m, true)
            }
        );
        order?.Validate();
        Assert.True(order?.IsValid());
        Assert.Equal(order?.Total(), 503.37m);
    }
}