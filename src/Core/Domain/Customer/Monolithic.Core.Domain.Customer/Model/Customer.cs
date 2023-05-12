using Monolithic.Core.Domain.Customer.Validator;
using Monolithic.Core.Domain.Customer.ValueObjects;
using Monolithic.Core.SharedKernel.DomainObjects;
namespace Monolithic.Core.Domain.Customer.Model;
public class Customer : AbstractEntity, IAggregateRoot
{
    private const byte VALID_CELLPHONE_LENGTH = 11;
    public Customer(string name,
                    string email,
                    string cellPhone,
                    Address address,
                    bool active)
    {
        Name = name;
        Email = email;
        CellPhone = cellPhone;
        Address = address;
        Active = active;
        Validate();
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CellPhone { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }
    public decimal RewardPoints { get; private set; }
    public void AddRewardPoints(decimal value) => RewardPoints += value;
    public void ChangeName(string value) => Name = value;
    public void ChangeEmail(string value) => Email = value;
    public void ChangeCellPhone(string value) => CellPhone = value;
    public void ChangeAddress(Address address) => Address = address;
    public void Activate(bool yes) => Active = yes;
    public void Disable(bool not) => Active = not;
    private bool MustBeValidCellPhone(string cellPhoneNumber)
        => cellPhoneNumber.Length >= VALID_CELLPHONE_LENGTH;
    public override bool IsValid()
    {
        var customerValidator = new CustomerValidator();
        ValidationResult = customerValidator.Validate(this);
        return ValidationResult.IsValid;
    }
}