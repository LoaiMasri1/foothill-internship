using ims.Models;

namespace ims.Providers;

public class InventoryProvider : IInventoryProvider
{
    private ICollection<Product> Products { get; init; }
    private static InventoryProvider? _instance;

    private InventoryProvider()
    {
        Products = new List<Product>();
    }

    public static InventoryProvider GetInstance()
    {
        _instance ??= new InventoryProvider();

        return _instance;
    }


    public bool AddProduct(Product product)
    {
        var exist = IsProductExists(product.Name);
        if (exist)
        {
            return false;
        }

        var newProduct = new Product
        {
            Name = product.Name.ToLower(),
            Price = product.Price,
            Quantity = product.Quantity
        };

        Products.Add(newProduct);

        return true;
    }

    public bool DeleteProduct(string productName)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            return false;
        }

        var product = GetProduct(productName);

        if (product is null)
        {
            return false;
        }

        Products.Remove(product);

        return true;
    }

    public Product? GetProduct(string productName)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            return null!;
        }

        var product = Products.FirstOrDefault(p => p.Name == productName.ToLower());
        return product;
    }

    public ICollection<Product> GetProducts()
    {
        var products = new List<Product>(Products);
        return products;
    }

    public bool UpdateProduct(string productName, Product product)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            return false;
        }

        var productToUpdate = GetProduct(productName);
        if (productToUpdate is null)
        {
            return false;
        }

        if (productToUpdate.Equals(product))
        {
            return false;
        }

        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.Quantity = product.Quantity;

        return true;
    }

    public bool IsProductExists(string productName)
    {
        var exist = Products.Any(p => p.Name == productName.ToLower());
        return exist;
    }
}
