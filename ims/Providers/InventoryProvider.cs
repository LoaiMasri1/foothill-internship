using ims.Helper.Exceptions;
using static ims.Helper.Exceptions.Constants.Messages;
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
            throw new AlreadyExistException(string.Format(ProductAlreadyExist, product.Name));
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
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
        }

        var product = GetProduct(productName);
        Products.Remove(product!);

        return true;
    }

    public Product? GetProduct(string productName)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
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
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
        }

        var productToUpdate = GetProduct(productName);
        if (productToUpdate is null)
        {
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
        }

        if (productToUpdate.Equals(product))
        {
            throw new AlreadyExistException(string.Format(ProductAlreadyExist, product.Name));
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
