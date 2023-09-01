using ims.Helper;
using ims.Providers;
using ims.Repositories;
using System.Configuration;

class Program
{
    private static readonly ConnectionStringSettings connectionStringSettings =
        ConfigurationManager.ConnectionStrings["foothill"] ??
        throw new ConfigurationErrorsException("foothill connection string not found");

    private static readonly IProductRepository productRepository = 
        new ProductRepository(
               connectionStringSettings.ConnectionString);

    private static readonly IInventoryProvider inventoryProvider = 
        new InventoryProvider(productRepository);
    private static readonly AppMenu _appMenu = new(inventoryProvider);

    static void Main(string[] args)
    {
        while (true)
        {
            AppMenu.ShowMenuOptions();

            var option = Console.ReadLine();
            if (option is null || option == string.Empty)
            {
                Console.WriteLine("Please enter a valid option");
                continue;
            }

            _appMenu.ProcessOption(option);
        }
    }
}