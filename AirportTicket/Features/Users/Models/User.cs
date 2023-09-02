using AirportTicket.Common;
using AirportTicket.Features.Users.Models.Enums;

using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Features.Users.Models;
public class User : BsonEntityID
{
    public Guid UserId { get; set; }


    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(20, ErrorMessage = "Password must be at most 20 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$"
    , ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one number")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    public UserRole Role { get; set; }


    public User(
        Guid userId,
        string name,
        string password,
        string email,
        UserRole role)
    {
        UserId = userId;
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }

    public static User Create(
        string name,
        string password,
        string email,
        UserRole role)
    {
        var user = new User(
            Guid.NewGuid(),
            name,
            password,
            email,
            role);

        return user;
    }

    public override string ToString()
    {
        return $"Id: {UserId}, Name: {Name}, Email: {Email}, Role: {Role}";
    }

}
