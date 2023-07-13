using ims.Models;
using ims.Providers;

namespace ims.Helper;
public class AppMenu
{
    private readonly static InventoryProvider _inventoryProvider;
    private static readonly string[] _menu = new string[]
        {
        "1. Add Product",
        "2. Update Product",
        "3. Delete Product",
        "4. List Products",
        "5. Get Product",
        "6. Exit"
        };
    static AppMenu()
    {
        _inventoryProvider = InventoryProvider.GetInstance();
    }
    private static void AddProduct()
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

    private static void UpdateProduct()
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

    private static void DeleteProduct()
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

    private static void ListProducts()
    {
        var products = _inventoryProvider.GetProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("No products found");
            return;
        }

        products.ToList().ForEach(Console.WriteLine);
    }

    private static void GetProduct()
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
        foreach (var item in _menu)
        {
            Console.WriteLine(item);
        }
    }

    public static void ProcessOption(string option)
    {
        switch (option)
        {
            case "1":
                AddProduct();
                break;
            case "2":
                UpdateProduct();
                break;
            case "3":
                DeleteProduct();
                break;
            case "4":
                ListProducts();
                break;
            case "5":
                GetProduct();
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
