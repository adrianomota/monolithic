using FluentValidation;
namespace Monolithic.Core.Domain.Order.Validator;
public class OrderValidator : AbstractValidator<Model.Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.CustomerId)
            .NotNull()
            .Must(BeValidGuid)
            .WithMessage("Customer id should be valid");
        RuleFor(order => order.Items.Count)
            .GreaterThan(0)
            .WithMessage("Order item must be at least one item");
        RuleFor(order => order.Total())
            .GreaterThan(0);
    }
    private bool BeValidGuid(Guid value) => value != Guid.Empty;
}