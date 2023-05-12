using FluentValidation.Results;
using Monolithic.Core.Domain.Customer.Validator;
using Monolithic.Core.SharedKernel.DomainObjects;
namespace Monolithic.Core.Domain.Customer.ValueObjects;
public class Address : IValueObject
{
    public Address(string street, 
                   string number, 
                   string zipCode, 
                   string country)
    {
        Street = street;
        Number = number;
        ZipCode = zipCode;
        Country = country;
    }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string ZipCode { get; private set; }
    public string Country { get; private set; }
    public ValidationResult? ValidationResult  {  get;  set ; }
    public bool IsValid()
    {
        var addressValidator = new AddressValidator();
        ValidationResult = addressValidator.Validate(this);
        return ValidationResult.IsValid;
    }
    public void Validate() => IsValid();
}