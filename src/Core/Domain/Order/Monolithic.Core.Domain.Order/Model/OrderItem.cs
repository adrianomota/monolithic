using MongoDB.Bson.Serialization.Attributes;
using Monolithic.Core.Domain.Order.Validator;
using Monolithic.Core.SharedKernel.DomainObjects;

namespace Monolithic.Core.Domain.Order.Model;
public class OrderItem :  AbstractEntity
{
    public OrderItem(string name, int quantity, decimal price,bool active = true)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Validate();
    }

    [BsonElement("name")]
    public string Name { get; private set; }

    [BsonElement("quantity")]
    public int Quantity { get; private set; }
    
    [BsonElement("price")]
    public decimal Price { get; private set; }
    
    public void ChangeName(string value) => Name = value;
    public void ChangeQuantity(int value) => Quantity = value;
    public void ChangePrice(decimal value) => Price = value;

    public override bool IsValid()
    {
        var orderItemValidator = new OrderItemValidator();
        ValidationResult = orderItemValidator.Validate(this);
        return ValidationResult.IsValid;
    }
}