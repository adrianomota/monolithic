using Monolithic.Core.Domain.Product.Validator;
using Monolithic.Core.SharedKernel.DomainObjects;

namespace Monolithic.Core.Domain.Product.Model;
public class Product : AbstractEntity, IAggregateRoot
{
    public Product(string name, string description, decimal price, bool active)
    {
        Name = name;
        Description = description;
        Price = price;
        Active = active;
        Validate();
    }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public void ChangeName(string value) => Name = value;
    public void ChangeDescription(string value) => Description = value;
    public void ChangePrice(decimal value) => Price = value;
    public void Activate(bool yes) => Active = yes;
    public void Disable(bool not) => Active = not;
    public override bool IsValid()
    {
        var productValidator = new ProductValidator();
        ValidationResult = productValidator.Validate(this);
        return ValidationResult.IsValid;
    }
}