using ims.Helper;
using ims.Providers;
using ims.Repositories;
using Microsoft.Extensions.Configuration;
class Program
{
    private static readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    private static readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    private static readonly IProductRepository productRepository =
        new ProductRepository(
               _connectionString);

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