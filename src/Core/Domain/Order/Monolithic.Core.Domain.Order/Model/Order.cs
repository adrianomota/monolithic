using Monolithic.Core.Domain.Order.Validator;
using Monolithic.Core.SharedKernel.DomainObjects;

namespace Monolithic.Core.Domain.Order.Model;
public class Order : AbstractEntity, IAggregateRoot
{
    private readonly List<OrderItem> _orderItems;
    public Order(Guid customerId,bool active, IList<OrderItem> items)
    {
        CustomerId = customerId;
        _orderItems = new List<OrderItem>();
        _orderItems.AddRange(items);
        IsValid();
    }
    public Guid CustomerId { get; private set; }
    public IReadOnlyCollection<OrderItem> Items { get { return _orderItems.ToArray(); } }
    public void AddItem(OrderItem item) => _orderItems.Add(item);
    public void AddItems(IList<OrderItem> items) => _orderItems.AddRange(items);
    public void ChangeCustomer(Guid value) => CustomerId = value;
    public decimal Total() => _orderItems.Sum(p => p.Price * p.Quantity);
    public override bool IsValid()
    {
        var orderValidator = new OrderValidator();
        ValidationResult = orderValidator.Validate(this);
        return ValidationResult.IsValid;
    }
}