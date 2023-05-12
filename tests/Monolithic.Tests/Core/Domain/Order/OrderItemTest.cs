using FluentAssertions;

namespace Monolithic.Tests.Core.Domain.OrderItem;
[Collection(nameof(OrderItemCollection))]
public class OrderItemTest
{
    private readonly OrderItemFixture _orderItemFixture;
    public OrderItemTest(OrderItemFixture orderItemFixture)
    {
        _orderItemFixture = orderItemFixture;
    }

    [Fact]
    public void When_i_have_valid_orderItem_params_return_success()
    {
        var orderItem = _orderItemFixture.GetValidOrderItem();

        var expected = orderItem?.IsValid();

        Assert.True(expected);
    }


    [Fact]
    public void When_i_have_invalid_orderItem_params_return_error()
    {
        var orderItem = _orderItemFixture.GetinvalidOrderItem();
        var expected = orderItem?.IsValid();
        Assert.False(expected);
    }

    [Fact]
    public void When_i_have_two_items_of_ten_Real_should_return_twenty_in_the_total()
    {
        var orderItem1 = _orderItemFixture.GetValidOrderItem();
        orderItem1?.ChangePrice(10.0m);

        var orderItem2 = _orderItemFixture.GetValidOrderItem();
        orderItem2?.ChangePrice(10.0m);

        var result = orderItem1?.Price + orderItem2?.Price;

        result.Should().Be(20m);
    }

    [Fact]
    public void When_i_have_invalid_orderItem_with_quantity_equal_zero_return_error()
    {
        var orderItem = _orderItemFixture.GetValidOrderItem();
        orderItem?.ChangeQuantity(0);
        orderItem?.Validate();
        orderItem?.IsValid().Should().BeFalse();
        orderItem?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Quantity");
        orderItem?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Item quantity shoutd be greater than 0")
                                           .First().ErrorMessage
                                           .Should()
                                           .Be("Item quantity shoutd be greater than 0");
    } 

    [Fact]
    public void When_i_have_invalid_orderItem_with_price_equal_zero_return_error()
    {
        var orderItem = _orderItemFixture.GetValidOrderItem();
        orderItem?.ChangePrice(0.0m);
        orderItem?.Validate();
        orderItem?.IsValid().Should().BeFalse();
        orderItem?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Price");
        orderItem?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Item price shoutd be greater than 0")
                                           .First().ErrorMessage
                                           .Should()
                                           .Be("Item price shoutd be greater than 0");
    } 
}