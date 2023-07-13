using ims.Helper;

while (true)
{
    AppMenu.ShowMenuOptions();

    var option = Console.ReadLine();
    if (option is null)
    {
        Console.WriteLine("Please enter a valid option");
        continue;
    }

    AppMenu.ProcessOption(option);
}