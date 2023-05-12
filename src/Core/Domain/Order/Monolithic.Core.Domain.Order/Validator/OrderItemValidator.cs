using FluentValidation;
using Monolithic.Core.Domain.Order.Model;

namespace Monolithic.Core.Domain.Order.Validator;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(orderItem => orderItem.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product name is required");
        RuleFor(orderItem => orderItem.Quantity)
            .GreaterThan(default(int))
            .WithMessage("Item quantity shoutd be greater than 0");
        RuleFor(orderItem => orderItem.Price)
            .GreaterThan(default(decimal))
            .WithMessage("Item price shoutd be greater than 0");
    }
}