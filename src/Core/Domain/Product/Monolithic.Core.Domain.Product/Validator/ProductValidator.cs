using FluentValidation;

namespace Monolithic.Core.Domain.Product.Validator;
public class ProductValidator : AbstractValidator<Model.Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product name is required");
        RuleFor(product => product.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product description is required");
        RuleFor(product => product.Price)
            .GreaterThan(default(decimal))
            .WithMessage("Product price shoutd be greater than 0");
               
    }
}