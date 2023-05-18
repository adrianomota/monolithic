using MongoDB.Bson.Serialization.Attributes;
using Monolithic.Core.Domain.Order.Validator;
using Monolithic.Core.SharedKernel.DomainObjects;

namespace Monolithic.Core.Domain.Order.Model;
public class Order : AbstractEntity, IAggregateRoot
{
    public Order(string customerId,bool active, IList<OrderItem> items)
    {
        CustomerId = customerId;
        Items = new List<OrderItem>(); 
        IsValid();
    }
   
    [BsonElement("customerId")]
    public string CustomerId { get; private set; }
   
    [BsonElement("items")]
    public List<OrderItem> Items { get; set; }
    
    public void AddItem(OrderItem item) => Items.Add(item);
    public void AddItems(List<OrderItem> items) => Items.AddRange(items);
    public void ChangeCustomer(string value) => CustomerId = value;
    public decimal Total() => Items.Sum(p => p.Price * p.Quantity);
    public override bool IsValid()
    {
        var orderValidator = new OrderValidator();
        ValidationResult = orderValidator.Validate(this);
        return ValidationResult.IsValid;
    }
}