using CustomerModel = Monolithic.Core.Domain.Customer.Model;
using OrderModel = Monolithic.Core.Domain.Order.Model;
using Monolithic.Core.SharedKernel.DomainObjects;

namespace Monolithic.Core.Domain.Order.Service;
public static class OrderService
{
      public static Model.Order PlaceOrder(CustomerModel.Customer customer,IList<OrderModel.OrderItem> items)
    {
        if(items== null && items?.Count == 0) 
        {
            throw new DomainException("Order item must be at least one item");
        }   
        var order = new OrderModel.Order(customerId: customer.Id, active: true, items: items);
        customer.AddRewardPoints(order.Total() / 2);
        return order;
     }
    public static decimal GetTotal(IList<OrderModel.Order> orders)
    {
        decimal total = 0.0m;
        orders.ToList().ForEach(t => total +=t.Total());
        return total;
    }
}