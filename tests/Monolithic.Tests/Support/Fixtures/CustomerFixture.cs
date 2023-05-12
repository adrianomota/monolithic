using Bogus;
using Monolithic.Core.Domain.Customer.Model;
using Monolithic.Core.Domain.Customer.ValueObjects;
namespace Monolithic.Tests.Support.Fixtures;
public class CustomerFixture : IDisposable
{
    public CustomerFixture() { }

    public IEnumerable<Customer> GetValidCustomers()
    {
        var customers = new List<Customer>();
        customers.AddRange(GenerateValidCustomer(10, true));
        customers.AddRange(GenerateValidCustomer(10, false));
        return customers;
    }

    public Customer? GetValidCustomer()
        => GenerateValidCustomer(1, true).FirstOrDefault();

    public Customer? GetInvalidCustomer()
        => GenerateInvalidCustomer(1, true).FirstOrDefault();
        
    private IEnumerable<Customer> GenerateValidCustomer(int quantity,bool active)
    {
        var customer = new Faker<Customer>("pt_BR")
          .CustomInstantiator(f => new Customer(
              name: f.Person.FullName,
              email: f.Person.Email,
              cellPhone: f.Phone.PhoneNumberFormat(),
              address: new Address(
                  street: "Av Henriqueta Mendes Guerra",
                  number: "1330",
                  zipCode: "06401015",
                  country: "Brazil"
              ),
              active: true
          ));
        return customer.Generate(quantity);
    }

    private IEnumerable<Customer> GenerateInvalidCustomer(int quantity,bool active)
    {
        var customer = new Faker<Customer>("pt_BR")
          .CustomInstantiator(f => new Customer(
              name: "",
              email: "",
              cellPhone: "",
              address: new Address(
                  street: "",
                  number: "",
                  zipCode: "",
                  country: ""
              ),
              active: false
          ));
        return customer.Generate(quantity);
    }

    public void Dispose() { }
}