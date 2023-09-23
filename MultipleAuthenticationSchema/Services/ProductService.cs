using MultipleAuthenticationSchema.Models;

namespace MultipleAuthenticationSchema.Services;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new() { Id = 1, Name = "Apple iPhone 15 Pro" },
        new() { Id = 2, Name = "Samsung Galaxy S23 Ultra" }
    };

    public List<Product> GetList()
    {
        return _products;
    }
}