namespace AirportTicket.Common.Constants;

public static class PathConstants
{
    public static readonly string SampleDataPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "Storage",
        "sample.csv");

    public static readonly string DocumentsFolderPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

} 