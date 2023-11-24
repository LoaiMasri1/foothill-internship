namespace ims.Providers;
using ims.Models;

public interface IInventoryProvider
{
    ICollection<Product> GetProducts();
    Product? GetProduct(string productName);
    bool AddProduct(Product product);
    bool UpdateProduct(string productName, Product product);
    bool DeleteProduct(string productName);
    bool IsProductExists(string productName);
}
