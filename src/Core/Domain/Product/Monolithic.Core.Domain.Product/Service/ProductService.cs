namespace Monolithic.Core.Domain.Product.Service;
using Model = Monolithic.Core.Domain.Product.Model;
public static class ProductService
{
     public static IList<Model.Product> IncreasePrice(IList<Model.Product> products, decimal percentage)
    {
        products.ToList().ForEach(product =>
        {
            product.ChangePrice((product.Price * percentage) / 100 + product.Price);
        });
        return products;
    }
}