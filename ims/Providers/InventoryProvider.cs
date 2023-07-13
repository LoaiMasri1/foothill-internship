using ims.Models;

namespace ims.Providers;

public class InventoryProvider : IInventoryProvider
{
    public bool AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public bool DeleteProduct(string productName)
    {
        throw new NotImplementedException();
    }

    public Product GetProduct(string productName)
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> GetProducts()
    {
        throw new NotImplementedException();
    }

    public bool UpdateProduct(string productName, Product product)
    {
        throw new NotImplementedException();
    }
}
