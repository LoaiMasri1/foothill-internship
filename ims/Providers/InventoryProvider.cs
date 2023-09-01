using ims.Helper.Exceptions;
using static ims.Helper.Exceptions.Constants.Messages;
using ims.Models;
using ims.Repositories;

namespace ims.Providers;

public class InventoryProvider : IInventoryProvider
{
    private readonly IProductRepository _productRepository;

    public InventoryProvider(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool AddProduct(Product product)
    {
        var exist = IsProductExists(product.Name);

        if (exist)
        {
            throw new AlreadyExistException(string.Format(ProductAlreadyExist, product.Name));
        }

        var result = _productRepository.Create(product);

        return result;
    }

    public bool DeleteProduct(string productName)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
        }

        var result = _productRepository.Delete(productName);

        return result;
    }

    public Product? GetProduct(string productName)
    {
        var exist = IsProductExists(productName);
        if (!exist)
        {
            throw new NotFoundException(string.Format(ProductDoesNotExist, productName));
        }

        var product = _productRepository.Get(productName);

        return product;
    }

    public ICollection<Product> GetProducts()
    {
        var products = _productRepository.GetAll();
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

        var result = _productRepository.Update(product);

        return result;
    }

    public bool IsProductExists(string productName)
    {
        var result = _productRepository.IsExists(productName);

        return result;
    }
}
