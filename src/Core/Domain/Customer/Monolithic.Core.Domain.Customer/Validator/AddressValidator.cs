using FluentValidation;
namespace Monolithic.Core.Domain.Customer.Validator;
public class AddressValidator : AbstractValidator<ValueObjects.Address>
{
   public AddressValidator()
    {
        RuleFor(address => address.Street)
          .NotNull()
          .NotEmpty()
          .WithMessage("Customer address street is required");
        RuleFor(address => address.Number)
          .NotNull()
          .NotEmpty()
          .WithMessage("Customer address number is required");
        RuleFor(address => address.ZipCode)
          .NotNull()
          .NotEmpty()
          .WithMessage("Customer address zipcode is required");
        RuleFor(address => address.Country)
          .NotNull()
          .NotEmpty()
          .WithMessage("Customer address country is required");
    }    
}