using FluentAssertions;
using Monolithic.Tests.Support.Fixtures;

namespace Monolithic.Tests.Core.Domain.Customer;
[Collection(nameof(CustomerCollection))]
public class CustomerTest
{
    private readonly CustomerFixture _customerFixture;
    public CustomerTest(CustomerFixture customerFixture)
    {
        _customerFixture = customerFixture;
    }

    [Fact]
    public void When_i_have_valid_customer_params_return_success()
    {
        var customer = _customerFixture.GetValidCustomer();
        var expected = customer?.ValidationResult?.IsValid;
        Assert.True(expected);
    }

    [Fact]
    public void When_i_do_not_have_valid_customer_name_param_return_error()
    {
        var customer = _customerFixture.GetValidCustomer();
        customer?.ChangeName(string.Empty);
        customer?.Validate();
        customer?.ValidationResult?.IsValid.Should().BeFalse();
        customer?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Name");
        customer?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Customer name is required")
                                          .First().ErrorMessage
                                          .Should()
                                          .Be("Customer name is required");
    }

    [Fact]
    public void When_i_have_valid_customer_name_with_more_than_40_characters_param_return_error()
    {
         var customer = _customerFixture.GetValidCustomer();
         customer?.ChangeName("dadjshdljkksdhhahklsjahdjhsasajkdhjsahjdhsajsahdhjkahhsasadkjahdjkshadjk");
         customer?.Validate();
         customer?.IsValid().Should().BeFalse();
         customer?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Name");
         customer?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Customer name should be between 3 and 40 characteres")
                                           .First().ErrorMessage
                                           .Should()
                                           .Be("Customer name should be between 3 and 40 characteres");
    }

    [Fact]
    public void When_i_have_a_customer_email_empty_param_return_error()
    {
       var customer = _customerFixture.GetValidCustomer();
       customer?.ChangeEmail("");
       customer?.Validate();
       customer?.IsValid().Should().BeFalse();
       customer?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Email");
       customer?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Customer e-mail is required")
                                         .First().ErrorMessage
                                         .Should()
                                         .Be("Customer e-mail is required");
    }

    [Fact]
    public void When_i_do_not_have_valid_customer_email_param_return_error()
    {
       var customer = _customerFixture.GetValidCustomer();
       customer?.ChangeEmail("not_valid_email");
       customer?.Validate();
       customer?.IsValid().Should().BeFalse();
       customer?.ValidationResult?.Errors.Select(p => p.PropertyName).First().Should().Be("Email");
       customer?.ValidationResult?.Errors.Where(p => p.ErrorMessage == "Should be a valid Email")
                                         .First().ErrorMessage
                                         .Should()
                                         .Be("Should be a valid Email");
    }

    [Fact]
    public void When_i_have_valid_customer_could_add_reward_points()
    {
        var customer = _customerFixture.GetValidCustomer();
        customer?.RewardPoints.Should().Be(0m);

        customer?.AddRewardPoints(10);
        customer?.RewardPoints.Should().Be(10);

        customer?.AddRewardPoints(10);
        customer?.RewardPoints.Should().Be(20m);
    }
}
