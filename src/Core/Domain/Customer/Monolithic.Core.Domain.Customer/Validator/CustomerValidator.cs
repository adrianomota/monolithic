using System.Text.RegularExpressions;
using FluentValidation;
namespace Monolithic.Core.Domain.Customer.Validator;
public class CustomerValidator : AbstractValidator<Model.Customer>
{   
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Customer name is required");
        RuleFor(customer => customer.Name)
            .Length(3, 40)
            .WithMessage("Customer name should be between 3 and 40 characteres");
        RuleFor(customer => customer.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Customer e-mail is required");
        RuleFor(customer => customer.Email)
            .EmailAddress()
            .WithMessage("Should be a valid Email");
        RuleFor(customer => customer.CellPhone)
            .NotNull()
            .NotEmpty()
            .WithMessage("Customer cell phone is required");
        RuleFor(customer => customer.CellPhone)
            .Must(BeValidCellPhone)
            .WithMessage("Invalid Cell phone");
        RuleFor(customer => customer.Address)
           .SetValidator(new AddressValidator());
    }

    private bool BeValidCellPhone(string cellPhoneValue)
        => Regex.Match(cellPhoneValue, 
                "(\\(?\\d{2}\\)?\\s)?(\\d{4,5}\\-\\d{4})")
                .Success;
}