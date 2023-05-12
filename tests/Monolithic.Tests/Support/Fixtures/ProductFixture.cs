using Bogus;
using Monolithic.Core.Domain.Product.Model;

namespace Monolithic.Tests.Support.Fixtures;
public class ProductFixture : IDisposable
{
    public ProductFixture() { }
    public IEnumerable<Product> GetValidProducts()
    {
        var products = new List<Product>();
        products.AddRange(GenerateValidProduct(10, true));
        products.AddRange(GenerateValidProduct(10, false));
        return products;
    }

    public Product? GetValidProduct()
        => GenerateValidProduct(1, true).FirstOrDefault();

    public Product? GetInvalidProduct()
        => GenerateInvalidProduct(1, true).FirstOrDefault();

    private IEnumerable<Product> GenerateValidProduct(int quantity, bool active)
    {
        var product = new Faker<Product>("pt_BR")
            .CustomInstantiator(f => new Product(
                name: f.Commerce.ProductName(),
                description: f.Commerce.ProductDescription(),
                price: decimal.Parse(f.Commerce.Price()),
                active: active
            ));
        return product.Generate(quantity);
    }

    private IEnumerable<Product> GenerateInvalidProduct(int quantity, bool active)
    {
        var product = new Faker<Product>("pt_BR")
            .CustomInstantiator(f => new Product(
                name: "",
                description: "",
                price: 0,
                active: active
            ));
        return product.Generate(quantity);
    }

    public void Dispose()
    {
       
    }
}