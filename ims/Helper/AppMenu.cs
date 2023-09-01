using static ims.Helper.Exceptions.ExceptionHandler;
using ims.Models;
using ims.Providers;

namespace ims.Helper;
public class AppMenu
{
    private readonly IInventoryProvider _inventoryProvider;
    public AppMenu(IInventoryProvider inventoryProvider)
    {
        _inventoryProvider = inventoryProvider;
    }
    private static readonly string[] _menu = new string[]
        {
        "1. Add Product",
        "2. Update Product",
        "3. Delete Product",
        "4. List Products",
        "5. Get Product",
        "6. Exit"
        };

    private void AddProduct()
    {

        Console.WriteLine("Enter Product Name:");
        var name = Console.ReadLine()!;

        Console.WriteLine("Enter Product Price:");
        var price = Console.ReadLine();

        Console.WriteLine("Enter Product Quantity:");
        var quantity = Console.ReadLine();

        var product = new Product
        {
            Name = name,
            Price = Convert.ToDecimal(price),
            Quantity = Convert.ToInt32(quantity)
        };

        var result = _inventoryProvider.AddProduct(product);
        if (result)
        {
            Console.WriteLine("Product added successfully");
        }
        else
        {
            Console.WriteLine("Product not added");
        }
    }

    private void UpdateProduct()
    {
        Console.WriteLine("Enter Product Name to update:");
        var name = Console.ReadLine()!;

        Console.WriteLine("Enter Product Price:");
        var price = Console.ReadLine();

        Console.WriteLine("Enter Product Quantity:");
        var quantity = Console.ReadLine();

        var product = new Product
        {
            Name = name,
            Price = Convert.ToDecimal(price),
            Quantity = Convert.ToInt32(quantity)
        };

        var result = _inventoryProvider.UpdateProduct(name, product);
        if (result)
        {
            Console.WriteLine("Product updated successfully");
        }
        else
        {
            Console.WriteLine("Product not updated");
        }
    }

    private void DeleteProduct()
    {
        Console.WriteLine("Enter Product Name to delete:");
        var name = Console.ReadLine()!;

        var result = _inventoryProvider.DeleteProduct(name);
        if (result)
        {
            Console.WriteLine("Product deleted successfully");
        }
        else
        {
            Console.WriteLine("Product not deleted");
        }
    }

    private void ListProducts()
    {
        var products = _inventoryProvider.GetProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("No products found");
            return;
        }

        products.ToList().ForEach(Console.WriteLine);
    }

    private void GetProduct()
    {
        Console.WriteLine("Enter Product Name to get:");
        var productName = Console.ReadLine()!;

        var product = _inventoryProvider.GetProduct(productName);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine(product);
    }

    private static void Exit()
    {
        Console.WriteLine("Exiting...");
        Environment.Exit(0);
    }

    private static void InvalidOption()
    {
        Console.WriteLine("Invalid option selected");
    }

    public static void ShowMenuOptions()
    {
        Console.WriteLine("Please select an option:");
        _menu.ToList().ForEach(Console.WriteLine);
    }

    public void ProcessOption(string option)
    {
        switch (option)
        {
            case "1":
                Handle(AddProduct);
                break;
            case "2":
                Handle(UpdateProduct);
                break;
            case "3":
                Handle(DeleteProduct);
                break;
            case "4":
                Handle(ListProducts);
                break;
            case "5":
                Handle(GetProduct);
                break;
            case "6":
                Exit();
                break;
            default:
                InvalidOption();
                break;
        }
    }
}
