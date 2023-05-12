using FluentAssertions;
using Monolithic.Tests.Support.Fixtures;

namespace Monolithic.Tests.Core.Domain.Product;
[Collection(nameof(ProductCollection))]
public class ProductTest
{
    private readonly ProductFixture _productFixture;
    public ProductTest(ProductFixture productFixture)
    {
        _productFixture = productFixture;
    }

    [Fact]
    public void When_i_have_valid_product_params_return_success()
    {
        var product = _productFixture.GetValidProduct();
        Assert.True(product?.IsValid());
    }

    [Fact]
    public void When_i_have_invalidf_product_params_return_error()
    {
        var product = _productFixture.GetInvalidProduct();
        Assert.False(product?.IsValid());
    }

    [Fact]
    public void When_i_do_not_have_valid_product_name_param_return_error()
    {
        var product = _productFixture.GetValidProduct();
        product?.ChangeName(string.Empty);
        product?.Validate();
        product?.ValidationResult?.IsValid.Should().BeFalse();
        product?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Name");
        product?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Product name is required")
                                         .First().ErrorMessage
                                         .Should()
                                         .Be("Product name is required");
    }

    [Fact]
    public void When_i_do_not_have_valid_product_description_param_return_error()
    {
        var product = _productFixture.GetValidProduct();
        product?.ChangeDescription(string.Empty);
        product?.Validate();
        product?.ValidationResult?.IsValid.Should().BeFalse();
        product?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Description");
        product?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Product description is required")
                                         .First().ErrorMessage
                                         .Should()
                                         .Be("Product description is required");
    }

    [Fact]
    public void When_i_have_invalid_orderItem_with_price_equal_zero_return_error()
    {
        var product = _productFixture.GetValidProduct();
        product?.ChangePrice(0.0m);
        product?.Validate();
        product?.IsValid().Should().BeFalse();
        product?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Price");
        product?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Product price shoutd be greater than 0")
                                         .First().ErrorMessage
                                         .Should()
                                         .Be("Product price shoutd be greater than 0");
    }
}