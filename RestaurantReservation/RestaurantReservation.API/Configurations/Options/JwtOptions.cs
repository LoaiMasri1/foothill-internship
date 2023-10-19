namespace RestaurantReservation.API.Configurations.Options;
public class JwtOptions
{
    public const string SectionName = "Authentication:Schemes:Bearer";

    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
